using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    [SerializeField]
    private GameObject _cellPrefab;

    private Cell[] cells;

    public void Initialize(int columnCount, int rowCount)
    {
        cells = new Cell[columnCount];

        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            var spawnSpot = Instantiate(_cellPrefab, transform);
            spawnSpot.name = $"SpawnSpot_{columnIndex}";

            spawnSpot.transform.localPosition = new Vector3(columnCount / -2.0f + (columnIndex + 0.5f), rowCount / -2.0f + (rowCount + 0.5f), -1f);

            var cellComponent = spawnSpot.GetComponent<Cell>();
            cellComponent.Initialize(CellType.NonPlayable);

            cells[columnIndex] = cellComponent;
        }
    }

    internal Cell GetCell(int x)
    {
        return cells[x];
    }
}