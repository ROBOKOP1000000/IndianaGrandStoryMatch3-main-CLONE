using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    [SerializeField]
    private TouchInput _touchInput;

    [SerializeField]
    private Matcher _matcher;

    [SerializeField]
    private Tooling _tooling;

    private Booster _booster;

    public Cell[,] Cells;

    private List<Cell> _selections;

    public bool IsFalling;

    private bool _isSwapping;
    public bool IsMatching;

    public void Initialize(Cell[,] cells)
    {
        _selections = new List<Cell>();
        Cells = cells;
        _matcher.Initialize(cells, this);
        _booster = new Booster(Cells, _matcher);
        _tooling.Initialize(cells, _matcher.fall);
        _matcher.TryMatchAsync(_selections);
    }

    private void OnEnable()
    {
        _touchInput.SwipeLeft += LeftSwap;
        _touchInput.SwipeRight += RightSwap;
        _touchInput.SwipeUp += UpSwap;
        _touchInput.SwipeDown += DownSwap;

        EventHolder<Cell>.AddListener(Select);
    }

    private void OnDisable()
    {
        _touchInput.SwipeLeft -= LeftSwap;
        _touchInput.SwipeRight -= RightSwap;
        _touchInput.SwipeUp -= UpSwap;
        _touchInput.SwipeDown -= DownSwap;

        EventHolder<Cell>.RemoveListener(Select);
    }

    private async UniTask SwapAsync(Cell origin, Cell target)
    {
        _isSwapping = true;
        ResetSelections();

        await origin.AsyncMoveTo(target);

        if (origin.BoosterType == BoosterType.Moodlet)
        {
            await _booster.ActivateBooster(origin, target);
            _selections.Clear();
            _isSwapping = false;
            return;
        }
        if (target.BoosterType == BoosterType.Moodlet)
        {
            await _booster.ActivateBooster(target, origin);
            _selections.Clear();
            _isSwapping = false;
            return;
        }

        if (origin.ItemType == ItemType.Booster)
        {
            _booster.ActivateBooster(origin);
            origin.Deselect();
            _selections.Clear();
            _isSwapping = false;
            return;
        }

        if (target.ItemType == ItemType.Booster)
        {
            _booster.ActivateBooster(target);
            target.Deselect();
            _selections.Clear();
            _isSwapping = false;
            return;
        }

        if (!await _matcher.TryMatchAsync(_selections))
        {
            if (_tooling.IsHandActive)
            {
                _tooling.IsHandActive = false;
            }
            else
            {
                await origin.AsyncMoveTo(target);
            }
        }

        _selections.Clear();

        _isSwapping = false;
    }

    private async void Select(Cell cell)
    {
        if (GameManager.instance.isGameOver)
            return;
        if (_isSwapping || IsMatching || IsFalling)
        {
            cell.Deselect();
            return;
        }

        ResetSelections();

        if (_tooling.IsClawActive)
        {
            cell.Deselect();
            await _tooling.UseClaw(cell);
            await _matcher.TryMatchAsync(_selections);
            _selections.Clear();
            _isSwapping = false;
            _tooling.IsClawActive = false;
            return;
        }

        if (_tooling.IsHammerActive)
        {
            cell.Deselect();
            await _tooling.UseHammer(cell);
            await _matcher.TryMatchAsync(_selections);
            _selections.Clear();
            _isSwapping = false;
            _tooling.IsHammerActive = false;
            return;
        }


        if (_selections.Contains(cell))
        {
            if (cell.ItemType == ItemType.Booster)
            {
                await _booster.ActivateBooster(cell);
                _selections.Clear();
                _isSwapping = false;
            }
            return;
        }

        if (_selections.Count > 0)
        {
            if (Math.Abs(cell.X - _selections[0].X) == 1 && Math.Abs(cell.Y - _selections[0].Y) == 0 ||
                Math.Abs(cell.Y - _selections[0].Y) == 1 && Math.Abs(cell.X - _selections[0].X) == 0)
            {
                _selections.Add(cell);
            }
            else
            {
                ResetSelections();
                _selections.Clear();
                _selections.Add(cell);
            }
        }
        else
        {
            _selections.Add(cell);
        }

        if (_selections.Count < 2) return;

        await SwapAsync(_selections[0], _selections[1]);
    }

    private async void LeftSwap()
    {
        if (!IsCanSwipe())
            return;

        if (_selections.First().X <= 0)
            return;

        _selections.Add(Cells[_selections.First().X - 1, _selections.First().Y]);

        await SwapAsync(_selections.First(), Cells[_selections.First().X - 1, _selections.First().Y]);
    }

    private async void RightSwap()
    {
        if (!IsCanSwipe())
            return;

        if (_selections.First().X + 1 >= Cells.GetLength(0))
            return;

        _selections.Add(Cells[_selections.First().X + 1, _selections.First().Y]);

        await SwapAsync(_selections.First(), Cells[_selections.First().X + 1, _selections.First().Y]);
    }

    private async void UpSwap()
    {
        if (!IsCanSwipe())
            return;

        if (_selections.First().Y + 1 >= Cells.GetLength(1))
            return;

        _selections.Add(Cells[_selections.First().X, _selections.First().Y + 1]);

        await SwapAsync(_selections.First(), Cells[_selections.First().X, _selections.First().Y + 1]);
    }

    private async void DownSwap()
    {
        if (!IsCanSwipe())
            return;

        if (_selections.First().Y <= 0)
            return;

        _selections.Add(Cells[_selections.First().X, _selections.First().Y - 1]);

        await SwapAsync(_selections.First(), Cells[_selections.First().X, _selections.First().Y - 1]);
    }

    private bool IsCanSwipe()
    {
        if (_isSwapping || IsMatching || IsFalling)
            return false;

        ResetSelections();

        if (_selections.Count != 1)
            return false;

        return true;
    }

    private void ResetSelections()
    {
        if (!GameManager.instance.isGameOver)
        {
            foreach (var selection in _selections)
                selection.Deselect();
        }
    }
}