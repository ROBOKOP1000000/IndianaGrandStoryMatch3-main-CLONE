using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Booster
{
    private Cell[,] _cells;
    private Fall _fall;
    private Matcher _matcher;
    private bool IsMoodActive;
    private Cell _moodActive;

    public Booster()
    {
    }

    public Booster(Cell[,] cells, Matcher matcher)
    {
        this._cells = cells;
        _matcher = matcher;
        _fall = matcher.fall;
    }

    internal async UniTask ActivateBooster(Cell cell, Cell target = null)
    {
        _fall.IsFalling = true;
        switch (cell.BoosterType)
        {
            case BoosterType.SparkHorisontal:
                await UseHorisontalSpark(cell);
                break;

            case BoosterType.SparkVertical:
                await UseVerticalSpark(cell);
                break;

            case BoosterType.Plane:
                await UsePlane(cell);
                break;

            case BoosterType.Moodlet:
                await UseMoodlet(cell, target);

                break;

            case BoosterType.Bomb:
                await UseBomb(cell);

                break;
        }
    }

    private async UniTask UseMoodlet(Cell cell, Cell target)
    {
        if (target == null)
        {
            Debug.Log("Target is Null");
            await _fall.InitiateFall();
            await _matcher.TryMatchAsync();

            return;
        }

        var cellList = new List<Cell>();
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                cellList.Add(_cells[i, j]);
            }
        }

        var cells = cellList.Where(x => x.Item.Color == target.Item.Color).ToList();
        var deflateSequence = DOTween.Sequence();
        deflateSequence.Join(cell.Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
        cell.ClearCell();

        foreach (var defCell in cells)
        {
            deflateSequence.Join(defCell.Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
            defCell.ClearCell();
        }

        await deflateSequence.Play().AsyncWaitForCompletion();
        await _fall.InitiateFall();
        await _matcher.TryMatchAsync();
    }

    private async UniTask UseBomb(Cell cell)
    {
        var deflateSequence = DOTween.Sequence();
        float tweenDuration = 0.25f;

        for (int x = -2; x < 3; x++)
        {
            int defX = cell.X + x;
            if (defX < 0 || defX >= _cells.GetLength(0))
            {
                continue;
            }
            for (int y = -2; y < 3; y++)
            {
                int defY = cell.Y + y;
                if (defY < 0 || defY >= _cells.GetLength(1))
                {
                    continue;
                }
                deflateSequence.Join(_cells[defX, defY].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));

                _cells[defX, defY].ReleaseCell();
                //_cells[defX, defY].SwitchTileType(CellType.Free);
            }
        }
        await deflateSequence.Play().AsyncWaitForCompletion();
        await _fall.InitiateFall();
        await _matcher.TryMatchAsync();
    }

    private async UniTask UsePlane(Cell cell)
    {
        float tweenDuration = 0.25f;

        var deflateSequence = DOTween.Sequence();
        int randX = UnityEngine.Random.Range(0, _cells.GetLength(0));
        int randY = UnityEngine.Random.Range(0, _cells.GetLength(1));

        deflateSequence.Join(_cells[randX, randY].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
        //_cells[randX, randY].SwitchTileType(CellType.Free);
        _cells[randX, randY].ReleaseCell();

        deflateSequence.Join(_cells[cell.X, cell.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
        //_cells[cell.X, cell.Y].SwitchTileType(CellType.Free);
        _cells[cell.X, cell.Y].ReleaseCell();

        if (cell.X - 1 >= 0)
        {
            deflateSequence.Join(_cells[cell.X - 1, cell.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
            //_cells[cell.X - 1, cell.Y].SwitchTileType(CellType.Free);
            _cells[cell.X - 1, cell.Y].ReleaseCell();
        }

        if (cell.X + 1 < _cells.GetLength(0))
        {
            deflateSequence.Join(_cells[cell.X + 1, cell.Y].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
            //_cells[cell.X + 1, cell.Y].SwitchTileType(CellType.Free);
            _cells[cell.X + 1, cell.Y].ReleaseCell();
        }

        if (cell.Y - 1 >= 0)
        {
            deflateSequence.Join(_cells[cell.X, cell.Y - 1].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
            //_cells[cell.X, cell.Y - 1].SwitchTileType(CellType.Free);
            _cells[cell.X, cell.Y - 1].ReleaseCell();
        }

        if (cell.Y + 1 < _cells.GetLength(1))
        {
            deflateSequence.Join(_cells[cell.X, cell.Y + 1].Icon.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack));
            //_cells[cell.X, cell.Y + 1].SwitchTileType(CellType.Free);
            _cells[cell.X, cell.Y + 1].ReleaseCell();
        }

        await deflateSequence.Play().AsyncWaitForCompletion();
        await _fall.InitiateFall();
        await _matcher.TryMatchAsync();
    }

    private async UniTask UseHorisontalSpark(Cell cell)
    {
        var deflateSequence = DOTween.Sequence();
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            deflateSequence.Join(_cells[x, cell.Y].Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
            //_cells[x, cell.Y].CellType = CellType.Free;
            _cells[x, cell.Y].ReleaseCell();

        }

        await deflateSequence.Play().AsyncWaitForCompletion();
        await _fall.InitiateFall();
        await _matcher.TryMatchAsync();
    }

    private async UniTask UseVerticalSpark(Cell cell)
    {
        var deflateSequence = DOTween.Sequence();
        for (int y = 0; y < _cells.GetLength(1); y++)
        {
            deflateSequence.Join(_cells[cell.X, y].Icon.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack));
            //_cells[cell.X, y].CellType = CellType.Free;
            _cells[cell.X, y].ReleaseCell();
        }

        await deflateSequence.Play().AsyncWaitForCompletion();
        await _fall.InitiateFall();
        await _matcher.TryMatchAsync();
    }
}