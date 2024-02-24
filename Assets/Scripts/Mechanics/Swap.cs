//using Cysharp.Threading.Tasks;
//using DG.Tweening;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using UnityEngine;

//[Obsolete]
//public class Swap
//{
//    private float tweenDuration = 0.25f;
//    public Transform SwappingOverlay { get; set; }

//    private readonly List<ITile> _selection = new List<ITile>();
//    private readonly List<Cell> _selections = new List<Cell>();

//    public Cell[,] Cells { get; set; }
//    public Row SpawnRow { get; set; }
//    private Match _match;
//    private Fall _fall;

//    public delegate void PopHandler();

//    private PopHandler? notify;

//    public bool IsFalling;

//    private bool _isSwapping;
//    private bool _isMatching;

//    //private bool _isMoodActive;
//    //private Cell _moodActive;

//    public event PopHandler Notify
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

//    private Booster _booster;

//    public Swapper(Fall fall)
//    {
//        _fall = fall;
//    }

//    public async UniTask Select(Cell cell)
//    {
//        if (_isSwapping || _isMatching || IsFalling)
//        {
//            return;
//        }

//        _booster = new Booster(Cells);

//        if (_booster.IsMoodActive)
//        {
//            await _booster.UseBooster(cell);
//            notify.Invoke();
//            await TryMatchAsync();
//            return;
//        }

//        if (IsBooster(cell))
//        {
//            await _booster.UseBooster(cell);
//            notify.Invoke();
//            await TryMatchAsync();
//            return;
//        }
//        if (Tooling.Instance.IsClawActive)
//        {
//            _selections.Clear();
//            var deflateSequence = DOTween.Sequence();
//            deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

//            for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
//            {
//                if (GameManager.instance.tileTypeNeeds[i] == cell.Type)
//                {
//                    GameManager.instance.countNeed[i] -= 1;
//                }
//            }
//            cell.SwitchTileType(TileType.Free);

//            await deflateSequence.Play().AsyncWaitForCompletion();

//            notify.Invoke();
//            await TryMatchAsync();
//            Tooling.Instance.IsClawActive = false;
//            return;
//        }

//        if (Tooling.Instance.IsHammerActive)
//        {
//            _selections.Clear();

//            var deflateSequence = DOTween.Sequence();
//            deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
//            cell.SwitchTileType(TileType.Free);

//            for (int x = 0; x < Cells.GetLength(0); x++)
//            {
//                deflateSequence.Join(Cells[x, cell.Tile.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

//                for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
//                {
//                    if (GameManager.instance.tileTypeNeeds[i] == Cells[x, cell.Tile.Y].Type)
//                    {
//                        GameManager.instance.countNeed[i] -= 1;
//                    }
//                }
//                Cells[x, cell.Tile.Y].SwitchTileType(TileType.Free);
//            }

//            for (int y = 0; y < Cells.GetLength(1); y++)
//            {
//                deflateSequence.Join(Cells[cell.Tile.X, y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

//                for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
//                {
//                    if (GameManager.instance.tileTypeNeeds[i] == Cells[cell.Tile.X, y].Type)
//                    {
//                        GameManager.instance.countNeed[i] -= 1;
//                    }
//                }
//                Cells[cell.Tile.X, y].SwitchTileType(TileType.Free);
//            }
//            await deflateSequence.Play().AsyncWaitForCompletion();

//            notify.Invoke();
//            await TryMatchAsync();
//            Tooling.Instance.IsHammerActive = false;
//            return;
//        }

//        if (!_selections.Contains(cell))
//        {
//            if (_selections.Count > 0)
//            {
//                if (
//                Math.Abs(cell.Tile.X - _selections[0].Tile.X) == 1 &&
//                Math.Abs(cell.Tile.Y - _selections[0].Tile.Y) == 0 ||
//                Math.Abs(cell.Tile.Y - _selections[0].Tile.Y) == 1 &&
//                Math.Abs(cell.Tile.X - _selections[0].Tile.X) == 0)
//                {
//                    _selections.Add(cell);
//                    //  GameManager.instance.moves--;
//                }
//                else
//                {
//                    _selections.Clear();
//                    _selections.Add(cell);
//                }
//            }
//            else
//            {
//                _selections.Add(cell);
//            }
//        }

//        if (_selections.Count < 2) return;

//        _isSwapping = true;

//        await _selections[0].AsyncSwap(_selections[1]);

//        if (!await TryMatchAsync())
//        {
//            if (Tooling.Instance.IsHandActive)
//            {
//                Tooling.Instance.IsHandActive = false;
//            }
//            else
//            {
//                await _selections[0].AsyncSwap(_selections[1]);
//            }
//        }

//        _selections.Clear();
//        _isSwapping = false;
//    }

//    private bool IsBooster(Cell cell)
//    {
//        if (cell.Type == TileType.SparkHorisontal) return true;
//        if (cell.Type == TileType.SparkVertical) return true;
//        if (cell.Type == TileType.Plane) return true;
//        if (cell.Type == TileType.Moodlet) return true;
//        if (cell.Type == TileType.Bomb) return true;

//        return false;
//    }

//    private async UniTask<bool> TryMatchAsync()
//    {
//        var didMatch = false;
//        _isMatching = true;

//        var match = TileDataMatrixUtility.FindBestMatch(Cells);

//        while (match != null)
//        {
//            didMatch = true;

//            var tiles = match.Tiles;
//            var deflateSequence = DOTween.Sequence();

//            foreach (var tile in tiles) deflateSequence.Join(tile.Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

//            //Debug.Log($"MATCH");

//            foreach (var tile in tiles)
//            {
//                tile.SwitchTileType(TileType.Free);
//            }
//            await deflateSequence.Play()
//                                 .AsyncWaitForCompletion();

//            switch (match.Type)
//            {
//                case MatchType.FourHorisontal:
//                    SetBooster(tiles, TileType.SparkVertical);
//                    break;

//                case MatchType.FourVertical:
//                    SetBooster(tiles, TileType.SparkHorisontal);
//                    break;

//                case MatchType.Square:
//                    SetBooster(tiles, TileType.Plane);
//                    break;

//                case MatchType.FiveRow:
//                    SetBooster(tiles, TileType.Moodlet);
//                    break;

//                case MatchType.FiveFigure:
//                    SetBooster(tiles, TileType.Bomb);
//                    break;
//            }

//            notify.Invoke();
//            match = TileDataMatrixUtility.FindBestMatch(Cells);
//        }

//        _isMatching = false;
//        if (didMatch)
//        {
//            GameManager.instance.moves--;
//        }

//        return didMatch;
//    }

//    private void SetBooster(Cell[] tiles, TileType type)
//    {
//        if (_selections == null)
//        {
//            tiles[0].SwitchTileType(type);
//            return;
//        }

//        if (_selections.Count == 0)
//        {
//            tiles[0].SwitchTileType(type);
//            return;
//        }

//        if (_selections.Count == 1)
//        {
//            if (tiles.ToList().Contains(_selections[0]))
//            {
//                _selections[0].SwitchTileType(type);
//            }
//            else
//            {
//                tiles[0].SwitchTileType(type);
//            }
//            return;
//        }

//        if (_selections.Count == 2)
//        {
//            if (tiles.ToList().Contains(_selections[0]))
//            {
//                _selections[0].SwitchTileType(type);
//            }
//            else if (tiles.ToList().Contains(_selections[1]))
//            {
//                _selections[1].SwitchTileType(type);
//            }
//            else
//            {
//                tiles[0].SwitchTileType(type);
//            }
//        }
//    }
//}