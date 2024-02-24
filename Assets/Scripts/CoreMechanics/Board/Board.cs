using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private readonly int MINBOARDSIZE = 3;

    [SerializeField]
    private Vector2Int _boardSize;

    [SerializeField]
    private GameObject _columnPrefab;

    [SerializeField]
    private Swapper _swap;

    private Cell[,] _cells;

    public int Rows
    { get { return _boardSize.y; } }

    public int Columns
    { get { return _boardSize.x; } }

    private List<Column> _columns = new List<Column>();

    [SerializeField]
    private PlayableBoard playableBoard;

    [SerializeField]
    private bool _usePlayableBoard;

    [SerializeField]
    private bool _useGemColor;

    [SerializeField]
    private SpawnSpot _spawnSpot;

    [SerializeField]
    private SpriteCollection _spriteCollection;

    private void Awake()
    {
        _spriteCollection.Initialize();
        var renderer = GetComponent<SpriteRenderer>();
        var boardScale = GetComponent<BoardScale>();
        renderer.size = new Vector2(_boardSize.x, _boardSize.y);
        boardScale.ResizeBoard(_boardSize.x);

        playableBoard.Initialize(_boardSize.x, _boardSize.y);
        _spawnSpot.Initialize(_boardSize.x, _boardSize.y);

        CreateColumns();

        _cells = new Cell[_boardSize.x, _boardSize.y];
        for (int x = 0; x < _columns.Count; x++)
        {
            for (int y = 0; y < _columns[x].Cells.Count; y++)
            {
                _cells[x, y] = _columns[x].Cells[y];
                if (_usePlayableBoard)
                {
                    _cells[x, y].Initialize(playableBoard.CellTypes[x, y]);
                }
                else
                {
                    _cells[x, y].Initialize();
                }

                if (_useGemColor)
                {
                    if (playableBoard.ItemTypes[x, y] == ItemType.Gem)
                    {
                        _cells[x, y].SetItem(playableBoard.ItemTypes[x, y], playableBoard.GemColors[x, y]);
                    }
                    else if (playableBoard.ItemTypes[x, y] == ItemType.Dynamic)
                    {
                        _cells[x, y].SetItem(playableBoard.ItemTypes[x, y], playableBoard.ObstacleTypes[x, y]);
                    }
                    else if (playableBoard.ItemTypes[x, y] == ItemType.Static)
                    {
                        _cells[x, y].SetItem(playableBoard.ItemTypes[x, y], playableBoard.ObstacleTypes[x, y]);
                    }
                }
                else
                {
                    _cells[x, y].SetItem(ItemType.Gem, (GemColor)Random.Range(1, 6));
                }
            }
        }

        _swap.Initialize(_cells);
    }

    private void CreateColumns()
    {
        for (int columnIndex = 0; columnIndex < _boardSize.x; columnIndex++)
        {
            var column = Instantiate(_columnPrefab, transform);
            column.name = $"Column_{columnIndex}";

            column.transform.localPosition = new Vector3(_boardSize.x / -2.0f + (columnIndex + 0.5f), 0f, -1f);

            var columnRenderer = column.GetComponent<SpriteRenderer>();
            columnRenderer.size = new Vector2(1f, _boardSize.y);

            var columnComponent = column.GetComponent<Column>();
            columnComponent.Index = columnIndex;
            columnComponent.CreateCells(_boardSize.y);

            _columns.Add(columnComponent);
        }
    }

    private void OnValidate()
    {
        if (_boardSize.x <= MINBOARDSIZE) _boardSize.x = MINBOARDSIZE + 1;
        if (_boardSize.y <= MINBOARDSIZE) _boardSize.y = MINBOARDSIZE + 1;
    }

    public List<Cell> GetCellList()
    {
        var cells = new List<Cell>();
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                cells.Add(_cells[x, y]);
            }
        }
        return cells;
    }
}