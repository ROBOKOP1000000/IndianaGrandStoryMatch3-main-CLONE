//using DG.Tweening;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class Cell : MonoBehaviour
//{
//    public int X;
//    public int Y;

//    [SerializeField]
//    private TileType tileType;

//    [SerializeField]
//    private Image _selectionIcon;

//    public TileType Type { get => tileType; }

//    public ITile Tile;

//    public Image Icon;

//    public Button Button;

//    public delegate void SelectHandler(Cell cell);

//    private SelectHandler? notify;

//    public event SelectHandler Notify
//    {
//        add
//        {
//            notify += value;
//        }

//        remove
//        {
//            notify -= value;
//        }
//    }

//    private void Start()
//    {
//        //Button.onClick.AddListener(delegate { Select(); });

//        //InitTile(Type);
//    }

//    public void InitCell()
//    {
//        Button.onClick.AddListener(delegate { Select(); });
//        InitTile(Type);
//    }

//    private void Select()
//    {
//        notify.Invoke(this);
//        //_selectionIcon.gameObject.SetActive(true);
//    }

//    public void DeSelect()
//    {
//        _selectionIcon.gameObject.SetActive(false);
//    }

//    public async Task AsyncSwap(Cell target, float tweenDuration = 0.25f)
//    {

//        var sequence = DOTween.Sequence();

//        sequence.Join(Icon.transform.DOMove(target.Icon.transform.position, tweenDuration).SetEase(Ease.OutBack)).
//                 Join(target.Icon.transform.DOMove(Icon.transform.position, tweenDuration).SetEase(Ease.OutBack));
//        await sequence.Play()
//                      .AsyncWaitForCompletion();

//        Swap(target);
//    }

//    private void Swap(Cell target)
//    {
//        Icon.transform.SetParent(target.transform);
//        target.Icon.transform.SetParent(transform);

//        Button.targetGraphic = target.Icon;
//        target.Button.targetGraphic = Icon;

//        var icon = Icon;
//        Icon = target.Icon;
//        target.Icon = icon;

//        Tile.Icon = Icon;
//        target.Tile.Icon = icon;

//        var tile1Item = Tile.Type;
//        SwitchTileType(target.Type);
//        target.SwitchTileType(tile1Item);
//    }

//    public void InitTile(TileType type)
//    {
//        tileType = type;

//        switch (type)
//        {
//            case TileType.Empty:
//                Tile = new TileEmpty(Icon);
//                break;

//            case TileType.Stone:
//                Tile = new TileStone(Icon, Button, type);
//                break;

//            case TileType.Red:
//            case TileType.Green:
//            case TileType.Blue:
//            case TileType.Pink:
//            case TileType.Yellow:
//                Tile = new TileJewel(Icon, Button, type);

//                break;
//        }
//    }

//    public void SwitchTileType(TileType type)
//    {

//        if (type != TileType.Free)
//        {
//            Icon.transform.localScale = Vector3.one;
//        }

//        Tile.Type = type;
//        tileType = type;

//        if (type == TileType.SparkVertical)
//        {
//            Icon.sprite = SpriteCollection.Instance.DoubleArrowVertical;
//            return;
//        }

//        if (type == TileType.SparkHorisontal)
//        {
//            Icon.sprite = SpriteCollection.Instance.DoubleArrowHorisontal;
//            return;
//        }

//        if (type == TileType.Plane)
//        {
//            Icon.sprite = SpriteCollection.Instance.PaperPlane;
//            return;
//        }

//        if (type == TileType.Moodlet)
//        {
//            Icon.sprite = SpriteCollection.Instance.Coin;
//            return;
//        }

//        if (type == TileType.Bomb)
//        {
//            Icon.sprite = SpriteCollection.Instance.Bomb;
//            return;
//        }

//    }

//    internal void SetPosition(int x, int y)
//    {
//        Tile.X = x;
//        Tile.Y = y;

//        X = Tile.X;
//        Y = Tile.Y;
//    }
//}