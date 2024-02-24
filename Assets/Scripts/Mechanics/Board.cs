//using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

//public class Board : MonoBehaviour
//{
//    public static Board Instance { get; private set; }

//    public Cell[,] Cells;
//    public List<Cell> CellList;

//    [SerializeField]
//    private Row[] _rows;

//    [SerializeField]
//    private Row _spawnRow;

//    private Swapper _swap;

//    [SerializeField]
//    public Transform SwappingOverlay;

//    [SerializeField]
//    public Fall fall;

//    private void Awake() => Instance = this;

//    private void Start()
//    {
//        _swap = new Swapper(fall);
//        _swap.SwappingOverlay = SwappingOverlay;
//        _swap.SpawnRow = _spawnRow;

//        fall.Swapper = _swap;
//        CellList = new List<Cell>();

//        Cells = new Cell[_rows[0].GetCellsCount(), _rows.Length];

//        for (int y = 0; y < _rows.Length; y++)
//        {
//            for (int x = 0; x < _rows[y].GetCellsCount(); x++)
//            {
//                var cell = _rows[y].GetCell(x);
//                cell.InitCell();
//                //cell.Tile.Notify += _swap.Select; ;
//                //cell.Notify += _swap.Select; ;
//                cell.SetPosition(x, y);
//                Cells[x, y] = cell;
//                CellList.Add(cell);
//            }
//        }
//        _swap.Cells = Cells;
//        _swap.Notify += FillFree;
//    }

//    private void FillFree()
//    {
//        fall.InitiateFall();
//    }
//}