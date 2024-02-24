using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Matcher : MonoBehaviour
{
    public Cell[,] Cells { get; set; }
    public Fall fall;

    private List<Cell> _selections;
    private Swapper _swap;

    public void Initialize(Cell[,] cells, Swapper swap)
    {
        _swap = swap;
        Cells = cells;

        fall.Initialize(cells, swap);
    }

    public async UniTask<bool> TryMatchAsync(List<Cell> cells = null)
    {
        _selections = cells;
        var didMatch = false;
        _swap.IsMatching = true;

        var match = TileDataMatrixUtility.FindBestMatch(Cells);

        while (match != null)
        {
            didMatch = true;

            var tiles = match.Tiles;

            switch (match.Type)
            {
                case ItemMatchType.FourHorisontal:
                    SetBooster(tiles, BoosterType.SparkVertical);
                    break;

                case ItemMatchType.FourVertical:
                    SetBooster(tiles, BoosterType.SparkHorisontal);
                    break;

                case ItemMatchType.Square:
                    SetBooster(tiles, BoosterType.Plane);
                    break;

                case ItemMatchType.FiveRow:
                    SetBooster(tiles, BoosterType.Moodlet);
                    break;

                case ItemMatchType.FiveFigure:
                    SetBooster(tiles, BoosterType.Bomb);
                    break;
            }

            var deflateSequence = DOTween.Sequence();

            foreach (var tile in tiles)
            {
                if (tile.ItemType == ItemType.Booster)
                {
                }
                else if (tile.ItemType == ItemType.Gem)
                {
                    deflateSequence.Join(tile.Icon.transform.DOScale(Vector3.zero, 0.20f).SetEase(Ease.InBack));
                    //tile.CellType = CellType.Free;
                    tile.ReleaseCell();
                }

                if (tile.X - 1 > 0)
                {
                    CheckStone(Cells[tile.X - 1, tile.Y], deflateSequence);
                }
                if (tile.Y - 1 > 0)
                {
                    CheckStone(Cells[tile.X, tile.Y - 1], deflateSequence);
                }
                if (tile.X + 1 < Cells.GetLength(0))
                {
                    CheckStone(Cells[tile.X + 1, tile.Y], deflateSequence);
                }
                if (tile.Y + 1 < Cells.GetLength(1))
                {
                    CheckStone(Cells[tile.X, tile.Y + 1], deflateSequence);
                }
            }

            await deflateSequence.Play()
                                 .AsyncWaitForCompletion();

            await fall.InitiateFall();
            match = TileDataMatrixUtility.FindBestMatch(Cells);
        }

        _swap.IsMatching = false;
        if (didMatch)
        {
            GameManager.instance.moves--;
        }

        return didMatch;
    }

    private void CheckStone(Cell cell, Sequence deflateSequence)
    {
        if (cell.ItemType == ItemType.Dynamic && cell.ObstacleType == ObstacleType.Stone)
        {
            deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, 0.20f).SetEase(Ease.InBack));
            //cell.CellType = CellType.Free;
            cell.ReleaseCell();
        }

        if (cell.ItemType == ItemType.Static && cell.ObstacleType == ObstacleType.Tile)
        {
            if (cell.HitCell())
            {
                deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, 0.20f).SetEase(Ease.InBack));
            }
        }
    }

    private void SetBooster(Cell[] tiles, BoosterType type)
    {
        if (_selections == null || _selections.Count == 0)
        {
            tiles[0].SetItem(ItemType.Booster, type);
            return;
        }

        if (_selections.Count == 1)
        {
            if (tiles.ToList().Contains(_selections[0]))
            {
                _selections[0].SetItem(ItemType.Booster, type);
            }
            else
            {
                tiles[0].SetItem(ItemType.Booster, type);
            }
            return;
        }

        if (_selections.Count == 2)
        {
            if (tiles.ToList().Contains(_selections[0]))
            {
                _selections[0].SetItem(ItemType.Booster, type);
            }
            else if (tiles.ToList().Contains(_selections[1]))
            {
                _selections[1].SetItem(ItemType.Booster, type);
            }
            else
            {
                tiles[0].SetItem(ItemType.Booster, type);
            }
        }
    }
}