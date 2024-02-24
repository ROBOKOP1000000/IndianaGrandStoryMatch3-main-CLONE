using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Analytics;

public class Tooling : MonoBehaviour
{
    //public static Tooling Instance { get; private set; }
    private static string clawKey = "ClawCount";
    private static string hammerKey = "HammerCount";
    private static string handKey = "HandCount";

    [SerializeField]
    private Button _clawButton;

    [SerializeField]
    private Button _hammerButton;

    [SerializeField]
    private Button _handButton;
    private Fall _fall;

    public bool IsClawActive { get; set; }

    public bool IsHammerActive { get; set; }

    public bool IsHandActive { get; set; }

    private Cell[,] _cells;

    private void Awake()
    {
        //Instance = this;
        _clawButton = GameObject.Find("Claw").GetComponent<Button>();
        _hammerButton = GameObject.Find("Hammer").GetComponent<Button>();
        _handButton = GameObject.Find("Hand").GetComponent<Button>();
        
        if (!PlayerPrefs.HasKey(clawKey))
            PlayerPrefs.SetInt(clawKey, 3);
        if (!PlayerPrefs.HasKey(hammerKey))
            PlayerPrefs.SetInt(hammerKey, 3);
        if (!PlayerPrefs.HasKey(handKey))
            PlayerPrefs.SetInt(handKey, 3);

        _clawButton.onClick.AddListener(ActivateClaw);
        _hammerButton.onClick.AddListener(ActivateHammer);
        _handButton.onClick.AddListener(ActivateHand);
    }

    private void ActivateClaw()
    {
        IsClawActive = !IsClawActive;
    }

    private void ActivateHammer()
    {
        IsHammerActive = !IsHammerActive;
    }

    private void ActivateHand()
    {
        IsHandActive = !IsHandActive;
    }

    internal void Initialize(Cell[,] cells, Fall fall)
    {
        _cells = cells;
        _fall = fall;
    }

    public async UniTask UseClaw(Cell cell)
    {
        if (PlayerPrefs.GetInt(clawKey) > 0)
        {
            var deflateSequence = DOTween.Sequence();
            deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
            await deflateSequence.Play().AsyncWaitForCompletion();

            FirebaseManager.Instance.FirstRunToolUse();
            //cell.CellType = CellType.Free;
            cell.ReleaseCell();
            await _fall.InitiateFall();
            
            PlayerPrefs.SetInt(clawKey, PlayerPrefs.GetInt(clawKey) - 1);
            Debug.Log($"Claw uses left: {PlayerPrefs.GetInt(clawKey)}");
        }
    }

    public async UniTask UseHammer(Cell cell)
    {
        if (PlayerPrefs.GetInt(hammerKey) > 0)
        {
            var deflateSequence = DOTween.Sequence();
            deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));

            FirebaseManager.Instance.FirstRunToolUse();
            //cell.CellType = CellType.Free;
            cell.ReleaseCell();

            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                deflateSequence.Join(_cells[x, cell.Y].Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
                //_cells[x, cell.Y].CellType = CellType.Free;
                _cells[x, cell.Y].ReleaseCell();
            }

            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                deflateSequence.Join(_cells[cell.X, y].Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
                //_cells[cell.X, y].CellType = CellType.Free;
                _cells[cell.X, y].ReleaseCell();
            }

            await deflateSequence.Play().AsyncWaitForCompletion();

            await _fall.InitiateFall();
            
            PlayerPrefs.SetInt(hammerKey, PlayerPrefs.GetInt(hammerKey) - 1);
            Debug.Log($"Hammer uses left: {PlayerPrefs.GetInt(hammerKey)}");
        }
    }

    public void UseHand()
    {
        if (PlayerPrefs.GetInt(handKey) > 0)
        {
            PlayerPrefs.SetInt(handKey, PlayerPrefs.GetInt(handKey) - 1);
            Debug.Log($"Hand uses left: {PlayerPrefs.GetInt(handKey)}");
        }
    }
}