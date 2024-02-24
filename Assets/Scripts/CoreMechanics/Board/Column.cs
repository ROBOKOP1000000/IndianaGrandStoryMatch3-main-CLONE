using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public int Index;

    [SerializeField]
    private GameObject _cellPrefab;

    private List<Cell> _cells = new List<Cell>();

    public List<Cell> Cells
    {
        get
        {
            return _cells;
        }
    }

    public void CreateCells(int cellCount)
    {
        for (int i = 0; i < cellCount; i++)
        {
            var cell = Instantiate(_cellPrefab, transform);
            cell.name = $"Cell_({Index},{i})";
            cell.transform.localPosition = new Vector3(0f, cellCount / -2.0f + (i + 0.5f), -1f);
            var component = cell.GetComponent<Cell>();
            component.X = Index;
            component.Y = i;
            _cells.Add(component);
        }
    }
}