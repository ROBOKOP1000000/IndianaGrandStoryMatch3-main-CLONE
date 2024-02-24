//using DG.Tweening;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UnityEngine;

//public class Booster
//{
//    private float tweenDuration = 0.25f;
//    public bool IsMoodActive;
//    private Cell _moodActive;
//    public Cell[,] Cells { get; set; }

//    public Booster(Cell[,] cells)
//    {
//        Cells = cells;
//    }

//    public async Task UseBooster(Cell cell)
//    {
//        if (IsMoodActive)
//        {
//            var cells = Board.Instance.CellList.Where(x => x.Type == cell.Tile.Type).ToList();
//            var deflateSequence = DOTween.Sequence();
//            deflateSequence.Join(_moodActive.Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//            _moodActive.SwitchTileType(TileType.Free);

//            foreach (var defCell in cells)
//            {
//                deflateSequence.Join(defCell.Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                defCell.SwitchTileType(TileType.Free);
//            }
//            await deflateSequence.Play().AsyncWaitForCompletion();

//            _moodActive = null;
//            IsMoodActive = false;
//        }

//        if (cell.Type == TileType.SparkHorisontal)
//        {
//            var deflateSequence = DOTween.Sequence();
//            for (int x = 0; x < Cells.GetLength(0); x++)
//            {
//                deflateSequence.Join(Cells[x, cell.Tile.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[x, cell.Tile.Y].SwitchTileType(TileType.Free);
//            }

//            await deflateSequence.Play().AsyncWaitForCompletion();
//        }

//        if (cell.Type == TileType.SparkVertical)
//        {
//            var deflateSequence = DOTween.Sequence();
//            for (int y = 0; y < Cells.GetLength(1); y++)
//            {
//                deflateSequence.Join(Cells[cell.Tile.X, y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[cell.Tile.X, y].SwitchTileType(TileType.Free);
//            }

//            await deflateSequence.Play().AsyncWaitForCompletion();
//        }

//        if (cell.Type == TileType.Plane)
//        {
//            var deflateSequence = DOTween.Sequence();
//            int randX = UnityEngine.Random.Range(0, Cells.GetLength(0));
//            int randY = UnityEngine.Random.Range(0, Cells.GetLength(1));

//            deflateSequence.Join(Cells[randX, randY].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//            Cells[randX, randY].SwitchTileType(TileType.Free);

//            deflateSequence.Join(Cells[cell.Tile.X, cell.Tile.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//            Cells[cell.Tile.X, cell.Tile.Y].SwitchTileType(TileType.Free);

//            if (cell.Tile.X - 1 >= 0)
//            {
//                deflateSequence.Join(Cells[cell.Tile.X - 1, cell.Tile.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[cell.Tile.X - 1, cell.Tile.Y].SwitchTileType(TileType.Free);
//            }

//            if (cell.Tile.X + 1 < Cells.GetLength(0))
//            {
//                deflateSequence.Join(Cells[cell.Tile.X + 1, cell.Tile.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[cell.Tile.X + 1, cell.Tile.Y].SwitchTileType(TileType.Free);
//            }

//            if (cell.Tile.Y - 1 >= 0)
//            {
//                deflateSequence.Join(Cells[cell.Tile.X, cell.Tile.Y - 1].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[cell.Tile.X, cell.Tile.Y - 1].SwitchTileType(TileType.Free);
//            }

//            if (cell.Tile.Y + 1 < Cells.GetLength(1))
//            {
//                deflateSequence.Join(Cells[cell.Tile.X, cell.Tile.Y + 1].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                Cells[cell.Tile.X, cell.Tile.Y + 1].SwitchTileType(TileType.Free);
//            }

//            await deflateSequence.Play().AsyncWaitForCompletion();
//        }

//        if (cell.Type == TileType.Moodlet)
//        {
//            IsMoodActive = true;
//            _moodActive = cell;
//        }

//        if (cell.Type == TileType.Bomb)
//        {
//            var deflateSequence = DOTween.Sequence();

//            for (int x = -2; x < 3; x++)
//            {
//                int defX = cell.Tile.X + x;
//                if (defX < 0 || defX >= Cells.GetLength(0))
//                {
//                    continue;
//                }
//                for (int y = -2; y < 3; y++)
//                {
//                    int defY = cell.Tile.Y + y;
//                    if (defY < 0 || defY >= Cells.GetLength(1))
//                    {
//                        continue;
//                    }
//                    deflateSequence.Join(Cells[defX, defY].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//                    Cells[defX, defY].SwitchTileType(TileType.Free);
//                }
//            }
//            await deflateSequence.Play().AsyncWaitForCompletion();
//        }

//        //notify.Invoke();
//        //if (!await TryMatchAsync())
//        //{
//        //}
//    }
//}
