//using DG.Tweening;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class Fall : MonoBehaviour
//{
//    [SerializeField]
//    private Row _spawnRow;
//    public Swapper Swapper;

//    public async void InitiateFall()
//    {
//        Swapper.IsFalling= true;
//        var cells = Board.Instance.Cells;
//        var cellList = Board.Instance.CellList;
//        var freeCells = cellList.Where(x => x.Tile.Type == TileType.Free).ToList();

//        if (freeCells == null)
//        {
//            return;
//        }

//        if (freeCells?.Count() == 0)
//        {
//            return;
//        }

//        var fallcells = new List<Cell>();
//        foreach (var freeCell in freeCells)
//        {
//            var freeX = freeCell.Tile.X;
//            var freeY = freeCell.Tile.Y;

//            for (int y = freeY; y >= 0; y--)
//            {
//                if (cells[freeX, y].Tile.Type != TileType.Free && !fallcells.Contains(cells[freeX, y]))
//                {
//                    fallcells.Add(cells[freeX, y]);
//                }
//            }
//        }

//        foreach (var fallCell in fallcells)
//        {
//            var fallStep = freeCells.Where(x => x.Tile.X == fallCell.Tile.X && x.Tile.Y > fallCell.Tile.Y).Count();

//            var target = cells[fallCell.Tile.X, fallCell.Tile.Y + fallStep];

//            var sequence = DOTween.Sequence();

//            sequence.Join(fallCell.Icon.transform.DOMove(target.transform.position, 0.20f).SetEase(Ease.OutBack));

//            sequence.Play();

//            Swap(fallCell, target);
//        }

//        SpawnNewCells();
//    }

//    private void SpawnNewCells()
//    {
//        var cellList = Board.Instance.CellList;

//        var freeCells = cellList.Where(x => x.Tile.Type == TileType.Free).ToList();

//        foreach (var cell in freeCells)
//        {
//            cell.SwitchTileType((TileType)UnityEngine.Random.Range(2, 7));
//            var sequence = DOTween.Sequence();

//            sequence.Join(cell.Icon.transform.DOMove(_spawnRow.GetCell(cell.Tile.X).transform.position, 0.0f).SetEase(Ease.OutBack)).
//                     Join(cell.Icon.transform.DOMove(cell.transform.position, 0.20f).SetEase(Ease.OutBack));

//            sequence.Play();
//        }

//        Swapper.IsFalling = false;

//    }

//    private void Swap(Cell origin, Cell target)
//    {
//        origin.Icon.transform.SetParent(target.transform);
//        target.Icon.transform.SetParent(origin.transform);

//        origin.Button.targetGraphic = target.Icon;
//        target.Button.targetGraphic = origin.Icon;

//        var icon = origin.Icon;
//        origin.Icon = target.Icon;
//        target.Icon = icon;

//        origin.Tile.Icon = origin.Icon;
//        target.Tile.Icon = icon;

//        var tile1Item = origin.Tile.Type;
//        origin.SwitchTileType(target.Type);
//        target.SwitchTileType(tile1Item);
//    }
//}