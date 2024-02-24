using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public Vector2Int Position;
    public int X;
    public int Y;
    public CellType CellType;

    public Item Item;

    public GemColor ItemColor
    { get { return Item.Color; } }

    public ItemType ItemType
    { get { return Item.Type; } }

    public BoosterType BoosterType
    { get { return Item.BoosterType; } }

    public ObstacleType ObstacleType
    { get { return Item.ObstacleType; } }

    public event UnityAction CellSelected;

    [field: SerializeField]
    public SpriteRenderer SelectionSprite { get; private set; }

    [field: SerializeField]
    public SpriteRenderer Icon { get; private set; }

    private Vector3 _iconDefaultScale;

    private int _integrity;




    private void Awake()
    {
        SelectionSprite.enabled = false;
        _iconDefaultScale = Icon.transform.localScale;
    }


    private void OnMouseDown()
    {
        if (Item.Type == ItemType.Static || GameManager.instance.isGameOver)
            return;

        if (CellType == CellType.Playable)
        {
            Select();
            EventHolder<Cell>.RaiseRegistrationInfo(this);
        }
    }

    public void Initialize()
    {
        CellType = CellType.Playable;
        Item = new Item();
        Initialize(CellType);
    }

    public void Initialize(CellType type)
    {
        CellType = type;
        switch (type)
        {
            case CellType.NonPlayable:
                Icon.enabled = false;
                break;

            case CellType.Playable:
                break;

            case CellType.Free:
                break;

            case CellType.Empty:
                break;

            default:
                break;
        }

        Item = new Item();
    }

    private void Select()
    {
        SelectionSprite.enabled = true;
    }

    public void Deselect()
    {
        SelectionSprite.enabled = false;
    }

    internal void MoveTo(Cell target, float tweenDuration = 0.25f)
    {
        if (CellType != CellType.Playable)
            return;

        var sequence = DOTween.Sequence();

        sequence.Join(Icon.transform.DOMove(target.Icon.transform.position, tweenDuration).SetEase(Ease.OutBack)).
                 Join(target.Icon.transform.DOMove(Icon.transform.position, tweenDuration).SetEase(Ease.OutBack));

        sequence.Play();

        Swap(target);
    }

    internal async UniTask AsyncMoveTo(Cell target, float tweenDuration = 0.25f)
    {
        if (CellType != CellType.Playable)
            return;

        if (target.CellType != CellType.Playable)
            return;

        var sequence = DOTween.Sequence();

        sequence.Join(Icon.transform.DOMove(target.Icon.transform.position, tweenDuration).SetEase(Ease.OutBack)).
                 Join(target.Icon.transform.DOMove(Icon.transform.position, tweenDuration).SetEase(Ease.OutBack));

        await sequence.Play().AsyncWaitForCompletion();

        Swap(target);
    }

    internal async UniTask AsyncPop(float tweenDuration = 0.25f)
    {
        var sequence = DOTween.Sequence();

        sequence.Join(Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

        await sequence.Play().AsyncWaitForCompletion();
        Item.Type = ItemType.None;
    }

    public void Swap(Cell target)
    {
        Icon.transform.SetParent(target.transform);
        target.Icon.transform.SetParent(transform);

        var icon = Icon;
        Icon = target.Icon;
        target.Icon = icon;

        var item = target.Item;
        target.Item = Item;
        Item = item;

        var cellType = CellType;
        CellType = target.CellType;
        target.CellType = cellType;
    }

    internal void SetItem(ItemType gem, GemColor gemColor)
    {
        Icon.transform.localScale = _iconDefaultScale;
        Item.Color = gemColor;
        Item.Type = gem;
        if (Item.Type == ItemType.Gem)
        {
            switch (gemColor)
            {
                case GemColor.Red:
                    Icon.sprite = SpriteCollection.Instance.GemRed;
                    break;

                case GemColor.Green:
                    Icon.sprite = SpriteCollection.Instance.GemGreen;
                    break;

                case GemColor.Blue:
                    Icon.sprite = SpriteCollection.Instance.GemBlue;
                    break;

                case GemColor.Yellow:
                    Icon.sprite = SpriteCollection.Instance.GemYellow;
                    break;

                case GemColor.Pink:
                    Icon.sprite = SpriteCollection.Instance.GemPink;
                    break;

                default:
                    break;
            }
        }

        if (Item.Type == ItemType.Dynamic)
        {
        }
    }

    internal void SetItem(ItemType type, BoosterType boosterType)
    {
        Icon.transform.localScale = _iconDefaultScale;
        Item.Type = type;

        if (type == ItemType.Booster)
        {
            SetItemBooster(boosterType);
        }

        if (type == ItemType.Dynamic)
        {
        }
    }

    internal void SetItem(ItemType type, ObstacleType obstacle)
    {
        Icon.transform.localScale = _iconDefaultScale;
        Item.Type = type;
        Item.ObstacleType = obstacle;
        if (type == ItemType.Dynamic)
        {
            switch (obstacle)
            {
                case ObstacleType.None:
                    break;

                case ObstacleType.Stone:
                    Icon.sprite = SpriteCollection.Instance.Rock;
                    break;

                case ObstacleType.RedCat:
                    Icon.sprite = SpriteCollection.Instance.CatRed;
                    break;

                case ObstacleType.GreenCat:
                    Icon.sprite = SpriteCollection.Instance.CatGreen;
                    break;

                case ObstacleType.BlueCat:
                    Icon.sprite = SpriteCollection.Instance.CatBlue;
                    break;

                case ObstacleType.YellowCat:
                    Icon.sprite = SpriteCollection.Instance.CatYellow;
                    break;

                case ObstacleType.PinkCat:
                    Icon.sprite = SpriteCollection.Instance.CatPink;
                    break;

                case ObstacleType.Tile:
                    Icon.sprite = SpriteCollection.Instance.BoxUnharmed;

                    break;

                case ObstacleType.Ice:
                    break;

                default:
                    break;
            }
        }
        if (type == ItemType.Static)
        {
            switch (obstacle)
            {
                case ObstacleType.Tile:
                    Icon.sprite = SpriteCollection.Instance.BoxUnharmed;
                    _integrity = 3;
                    break;

                case ObstacleType.Ice:
                    break;

                default:
                    break;
            }
        }
    }

    private void SetItemBooster(BoosterType boosterType)
    {
        Item.BoosterType = boosterType;
        switch (boosterType)
        {
            case BoosterType.SparkHorisontal:
                Icon.sprite = SpriteCollection.Instance.BoosterSparkHorisontal;
                break;

            case BoosterType.SparkVertical:
                Icon.sprite = SpriteCollection.Instance.BoosterSparkVertical;
                break;

            case BoosterType.Plane:
                Icon.sprite = SpriteCollection.Instance.BoosterPlane;
                break;

            case BoosterType.Moodlet:
                Icon.sprite = SpriteCollection.Instance.BoosterCoin;
                break;

            case BoosterType.Bomb:
                Icon.sprite = SpriteCollection.Instance.BoosterBomb;
                break;

            default:
                break;
        }
    }

    internal void SwitchTileType(CellType type)
    {
        CellType = type;
    }

    internal void ReleaseCell()
    {
        print(Item.Color);
        if (GameManager.instance.isColor)
        {
            for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            {
                if (GameManager.instance.tileTypeNeeds[i] == Item.Color)
                {
                    GameManager.instance.countNeed[i] -= 1;
                }

            }
        }
        if (GameManager.instance.isStone)
        {
            for (int i = 0; i < GameManager.instance.obstacleTypeNeeds.Length; i++)
            {
                if (GameManager.instance.obstacleTypeNeeds[i] == Item.ObstacleType && Item.ObstacleType == ObstacleType.Stone && Item.Color == GemColor.None)
                {
                    GameManager.instance.countNeed[i] -= 1;
                }

            }
        }
        if (GameManager.instance.isAnubis)
        {
            for (int i = 0; i < GameManager.instance.obstacleTypeNeeds.Length; i++)
            {
                if (GameManager.instance.obstacleTypeNeeds[i] == Item.ObstacleType && Item.ObstacleType == ObstacleType.Tile && Item.Color == GemColor.None)
                {
                    GameManager.instance.countNeed[i] -= 1;
                }
            }
        }
        CellType = CellType.Free;
        Item.BoosterType = BoosterType.None;
        Item.Type = ItemType.Gem;
    }

    internal bool HitCell()
    {
        _integrity--;
        if (_integrity <= 0)
        {

            ReleaseCell();
            return true;
        }
        else if (_integrity == 1)
        {
            Icon.sprite = SpriteCollection.Instance.BoxBroken;

            return false;
        }
        else if (_integrity == 2)
        {
            Icon.sprite = SpriteCollection.Instance.BoxDamaged;
            return false;
        }
        return false;
    }

    internal void ClearCell()
    {
        CellType = CellType.Free;
        Item.BoosterType = BoosterType.None;
        Item.Color = GemColor.None;
        Item.Type = ItemType.None;
    }
}