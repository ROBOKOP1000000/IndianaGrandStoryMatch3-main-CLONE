using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField]
    private SpawnSpot _spawnRow;

    public Swapper _swapper;

    private Cell[,] _cells;
    private List<Cell> CellList;
    public bool IsFalling
    { get { return _swapper.IsFalling; } set { _swapper.IsFalling = value; } }

    public void Initialize(Cell[,] cells, Swapper swap)
    {
        _swapper = swap;
        _cells = cells;
        CellList = new List<Cell>();
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                CellList.Add(_cells[i, j]);
            }
        }
    }

    public async UniTask InitiateFall()
    {
        _swapper.IsFalling = true;
        var cells = _cells;
        var cellList = CellList;
        var freeCells = cellList.Where(x => x.CellType == CellType.Free).ToList();

        if (freeCells == null)
        {
            return;
        }

        if (freeCells?.Count() == 0)
        {
            return;
        }

        var fallcells = new List<Cell>();
        foreach (var freeCell in freeCells)
        {
            var freeX = freeCell.X;
            var freeY = freeCell.Y;

            for (int y = freeY; y < cells.GetLength(1); y++)
            {
                if (cells[freeX, y].CellType != CellType.Free && !fallcells.Contains(cells[freeX, y]))
                {
                    fallcells.Add(cells[freeX, y]);
                }
            }
        }

        var sequence = DOTween.Sequence();
        foreach (var fallCell in fallcells)
        {
            var fallStep = freeCells.Where(x => x.X == fallCell.X && x.Y < fallCell.Y).Count();

            var target = cells[fallCell.X, fallCell.Y - fallStep];

            sequence.Join(fallCell.Icon.transform.DOMove(target.transform.position, 0.20f).SetEase(Ease.OutBack));

            Swap(fallCell, target);
        }

        SpawnNewCells();

        await sequence.Play().AsyncWaitForCompletion();
        _swapper.IsFalling = false;
    }

    private void SpawnNewCells()
    {
        _swapper.IsFalling = true;

        var cellList = CellList;

        var freeCells = cellList.Where(x => x.CellType == CellType.Free).ToList();

        foreach (var cell in freeCells)
        {
            //cell.SwitchTileType((TileType)UnityEngine.Random.Range(2, 7));
            cell.CellType = CellType.Playable;
            cell.SetItem(ItemType.Gem, (GemColor)Random.Range(1, 6));
            var sequence = DOTween.Sequence();

            sequence.Join(cell.Icon.transform.DOMove(_spawnRow.GetCell(cell.X).transform.position, 0.0f).SetEase(Ease.OutBack)).
                     Join(cell.Icon.transform.DOMove(cell.transform.position, 0.20f).SetEase(Ease.OutBack));

            sequence.Play();
        }

        _swapper.IsFalling = false;
    }

    private void Swap(Cell origin, Cell target)
    {
        origin.Swap(target);
    }
}