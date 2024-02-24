using UnityEngine;

public class CellBase : MonoBehaviour
{

    [field: SerializeField] public float score { get; private set; } = 100f;

    private int X;
    private int Y;

    public int x
    {
        get { return X; }
        set
        {
            if (IsMovable())
            {
                X = value;
            }

        }
    }

    public int y
    {
        get { return Y; }
        set
        {
            if (IsMovable())
            {
                Y = value;
            }

        }
    }
    public GridManager.CellType cellType { get; set; }

    public GridManager grid { get; set; }

    public ColorGameCell.ColorType colorType { get; set; }

    public MovableGameCell movableGameCellComponent { get; private set; }
    public ColorGameCell colorGameCellComponent { get; private set; }
    public ClearableGameCell clearableGameCellComponent{ get; private set; }
    private void Awake()
    {
        movableGameCellComponent = GetComponent<MovableGameCell>();
        colorGameCellComponent = GetComponent<ColorGameCell>();
        clearableGameCellComponent = GetComponent<ClearableGameCell>();
    }


    public bool IsMovable()
    {
        return movableGameCellComponent != null;
    }
    public bool IsColored()
    {
        return colorGameCellComponent != null;
    }
    public bool IsClearadle()
    {
        return clearableGameCellComponent != null;
    }
    public void Init(int _x, int _y, GridManager _grid, GridManager.CellType _cellType, ColorGameCell.ColorType _colorType)
    {
        x = _x;
        y = _y;
        grid = _grid;
        cellType = _cellType;
        colorType = _colorType;
    }



    private void OnMouseEnter()
    {
        grid.EnteredCell(this);
    }
    private void OnMouseDown()
    {
        grid.PressedCell(this);
    }

    private void OnMouseUp()
    {
        grid.ReleaseCell();
    }
}
