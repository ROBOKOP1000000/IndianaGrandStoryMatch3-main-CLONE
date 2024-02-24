using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int xDimension = 9;
    [SerializeField] private int yDimension = 9;
    [SerializeField] private float fillTime = 0.1f;
    private bool inverse;
    private bool gameOver;

    [SerializeField] private List<int> counter = new List<int>();
    public CellToPrefab[] cellToPrefabs;
    public InitialCellPosition[] initialCells;
    public GameObject cellBackground;
    private Dictionary<CellType, CellBase> cellDictionary;
    private CellBase[,] cellsData;

    public LevelBase level;
    private CellBase pressedCell;
    private CellBase enteredCell;

    private void Awake()
    {
        cellDictionary = new Dictionary<CellType, CellBase>();

        for (int i = 0; i < cellToPrefabs.Length; i++)
        {
            if (!cellDictionary.ContainsKey(cellToPrefabs[i].type))
            {
                cellDictionary.Add(cellToPrefabs[i].type, cellToPrefabs[i].prefab);
            }
        }

        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                Instantiate(cellBackground, GetWorldPosition(x, y), Quaternion.identity, transform);
            }
        }

        cellsData = new CellBase[xDimension, yDimension];

        for (int i = 0; i < initialCells.Length; i++)
        {
            if (initialCells[i].x >= 0 && initialCells[i].x < xDimension && initialCells[i].y >= 0 && initialCells[i].y < yDimension)
            {
                SpawnNewGameCell(initialCells[i].x, initialCells[i].y, initialCells[i].type, initialCells[i].color);
            }
        }




        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                if (cellsData[x, y] == null)
                {
                    SpawnNewGameCell(x, y, CellType.EMPTY, ColorGameCell.ColorType.GREEN);
                }
            }
        }
        Check();
        //     StartCoroutine(FillGrid());
    }

    void Check()
    {
        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                Debug.Log($"some text {cellsData[x, y]},X value {x}, Y valye {y}");
            }
        }
    }
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(transform.position.x - xDimension / 2 + x, transform.position.y + yDimension / 2 - y);
    }

    public CellBase SpawnNewGameCell(int x, int y, CellType cellType, ColorGameCell.ColorType colorType)
    {
        CellBase newGameCell = Instantiate(cellDictionary[cellType], GetWorldPosition(x, y), Quaternion.identity, transform);

        newGameCell.Init(x, y, this, cellType, colorType);
        cellsData[x, y] = newGameCell;

        return cellsData[x, y];
    }

    public IEnumerator FillGrid()
    {
        bool needsRefill = true;
        while (needsRefill)
        {
            yield return new WaitForSeconds(fillTime);

            while (FillGridStep())
            {
                inverse = !inverse;
                yield return new WaitForSeconds(fillTime);
            }
            needsRefill = ClearAllValidMatches();
            print("Need " + needsRefill);
        }

    }

    public bool FillGridStep()
    {
        bool movedCell = false;
        for (int y = yDimension - 2; y >= 0; y--)
        {
            for (int loopX = 0; loopX < xDimension; loopX++)
            {
                int x = loopX;
                if (inverse)
                {
                    x = xDimension - 1 - loopX;
                }

                CellBase cell = cellsData[x, y];
                if (cell.IsMovable())
                {
                    CellBase cellBelow = cellsData[x, y + 1];
                    if (cellBelow.cellType == CellType.EMPTY) //если €чейка ниже пуста€ то двигаем текущую вниз
                    {
                        Destroy(cellBelow.gameObject);
                        cell.movableGameCellComponent.Move(x, y + 1, fillTime);
                        cellsData[x, y + 1] = cell;
                        SpawnNewGameCell(x, y, CellType.EMPTY, ColorGameCell.ColorType.ANY);
                        // Change
                        movedCell = true;
                    }
                    else
                    {
                        for (int diag = -1; diag <= 1; diag++)
                        {
                            if (diag == 0) continue;

                            int diagX = x + diag;

                            if (inverse)
                            {
                                diagX = x - diag;
                            }

                            if (diagX >= 0 && diagX < xDimension)
                            {
                                CellBase diagonalCell = cellsData[diagX, y + 1];
                                if (diagonalCell.cellType == CellType.EMPTY)
                                {
                                    bool hasCellAbove = true;
                                    for (int aboveY = y; aboveY >= 0; aboveY--)
                                    {
                                        CellBase CellAbove = cellsData[diagX, aboveY];
                                        if (CellAbove.IsMovable())
                                        {
                                            break;
                                        }
                                        else if (!CellAbove.IsMovable() && CellAbove.cellType != CellType.EMPTY)
                                        {
                                            hasCellAbove = false;
                                            break;
                                        }
                                    }

                                    if (!hasCellAbove)
                                    {
                                        Destroy(diagonalCell.gameObject);
                                        cell.movableGameCellComponent.Move(diagX, y + 1, fillTime);
                                        cellsData[diagX, y + 1] = cell;
                                        SpawnNewGameCell(x, y, CellType.EMPTY, ColorGameCell.ColorType.GREEN);
                                        movedCell = true;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        for (int x = 0; x < xDimension; x++)
        {
            CellBase cellBelow = cellsData[x, 0];
            if (cellBelow.cellType == CellType.EMPTY)
            {
                Destroy(cellBelow.gameObject);
                CellBase newGameCell = Instantiate(cellDictionary[CellType.NORMAL], GetWorldPosition(x, -1), Quaternion.identity, transform);
                //Change
                var rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    newGameCell.Init(x, -1, this, CellType.Red, ColorGameCell.ColorType.RED);

                }
                else if (rand == 1)
                {
                    newGameCell.Init(x, -1, this, CellType.Yellow, ColorGameCell.ColorType.YELLOW);

                }
                else
                {
                    newGameCell.Init(x, -1, this, CellType.Yellow, ColorGameCell.ColorType.YELLOW);
                }
                cellsData[x, 0] = newGameCell;
                cellsData[x, 0].movableGameCellComponent.Move(x, 0, fillTime);
                cellsData[x, 0].colorGameCellComponent.SetColor((ColorGameCell.ColorType)Random.Range(0, cellsData[x, 0].colorGameCellComponent.NumColors));
                movedCell = true;
            }
        }

        return movedCell;
    }



    public bool IsAdjacent(CellBase cell1, CellBase cell2)
    {
        return (cell1.x == cell2.x && (int)Mathf.Abs(cell1.y - cell2.y) == 1) || (cell1.y == cell2.y && (int)Mathf.Abs(cell1.x - cell2.x) == 1);
    }

    public void SwapCells(CellBase cell1, CellBase cell2)
    {
        if (gameOver) return;

        Debug.Log($"some text {cell1.IsMovable()},    {cell2.IsMovable()}");
        if (cell1.IsMovable() && cell2.IsMovable())
        {
            cellsData[cell1.x, cell1.y] = cell2;
            cellsData[cell2.x, cell2.y] = cell1;

            if (GetMatch(cell1, cell2.x, cell2.y) != null || GetMatch(cell2, cell1.x, cell1.y) != null || cell1.cellType == CellType.ANY_COLOR_BONUS || cell2.cellType == CellType.ANY_COLOR_BONUS)
            {
                int cell1X = cell1.x;
                int cell1Y = cell1.y;



                cell1.movableGameCellComponent.Move(cell2.x, cell2.y, fillTime);
                cell2.movableGameCellComponent.Move(cell1X, cell1Y, fillTime);

                if (cell1.cellType == CellType.ANY_COLOR_BONUS && cell1.IsClearadle() && cell2.IsColored())
                {
                    ClearAnyColorGameCell_Bonus clearColor = cell1.GetComponent<ClearAnyColorGameCell_Bonus>();

                    if (clearColor)
                    {
                        clearColor.color = cell2.colorGameCellComponent.Color;
                    }

                    ClearNormalCell(cell1.x, cell1.y);
                }

                if (cell2.cellType == CellType.ANY_COLOR_BONUS && cell2.IsClearadle() && cell1.IsColored())
                {
                    ClearAnyColorGameCell_Bonus clearColor = cell2.GetComponent<ClearAnyColorGameCell_Bonus>();

                    if (clearColor)
                    {
                        clearColor.color = cell1.colorGameCellComponent.Color;
                    }

                    ClearNormalCell(cell2.x, cell2.y);
                }


                //   ClearAllValidMatches();
                if (cell1.cellType == CellType.FOUR_ROW_BONUS || cell1.cellType == CellType.FOUR_COLOM_BONUS)
                {
                    ClearNormalCell(cell1.x, cell1.y);
                }
                if (cell2.cellType == CellType.FOUR_ROW_BONUS || cell2.cellType == CellType.FOUR_COLOM_BONUS)
                {
                    ClearNormalCell(cell2.x, cell2.y);
                }

                pressedCell = null;
                enteredCell = null;



                StartCoroutine(FillGrid());

                Check();
                level.OnMove();
            }
            else
            {
                // return originPosition
                cellsData[cell1.x, cell1.y] = cell1;
                cellsData[cell2.x, cell2.y] = cell2;
            }


        }
    }

    public void PressedCell(CellBase cell)
    {
        pressedCell = cell;
    }
    public void EnteredCell(CellBase cell)
    {
        enteredCell = cell;
    }

    public void ReleaseCell()
    {
        if (IsAdjacent(pressedCell, enteredCell))
        {
            SwapCells(pressedCell, enteredCell);
        }
    }

    public bool ClearNormalCell(int x, int y)
    {
        if (cellsData[x, y].IsClearadle() && !cellsData[x, y].clearableGameCellComponent.isBeingCleared)
        {
            cellsData[x, y].clearableGameCellComponent.Clear();
            SpawnNewGameCell(x, y, CellType.EMPTY, ColorGameCell.ColorType.GREEN);

            ClearObstacles(x, y);

            return true;
        }

        return false;
    }

    public bool ClearAllValidMatches()
    {
        bool needReFill = false;
        for (int y = 0; y < yDimension; y++)
        {
            for (int x = 0; x < xDimension; x++)
            {
                if (cellsData[x, y].IsClearadle())
                {
                    List<CellBase> match = GetMatch(cellsData[x, y], x, y);

                    if (match != null)
                    {
                        CellType specialCellType = CellType.COUNT;
                        //Change
                        ColorGameCell.ColorType colorType = ColorGameCell.ColorType.RED;
                        CellBase randomCell = match[Random.Range(0, match.Count)];
                        int specialCellX = randomCell.x;
                        int specialCellY = randomCell.y;

                        //создание бонусок
                        if (match.Count == 4)
                        {
                            if (pressedCell == null || enteredCell == null)
                            {
                                specialCellType = (CellType)Random.Range((int)CellType.FOUR_ROW_BONUS, (int)CellType.FOUR_COLOM_BONUS);
                            }
                            else if (pressedCell.y == enteredCell.y)
                            {
                                specialCellType = CellType.FOUR_ROW_BONUS;
                            }
                            else if (pressedCell.x == enteredCell.x)
                            {
                                specialCellType = CellType.FOUR_COLOM_BONUS;
                            }
                        }
                        else if (match.Count >= 5)
                        {
                            specialCellType = CellType.ANY_COLOR_BONUS;
                        }


                        for (int i = 0; i < match.Count; i++)
                        {
                            if (ClearNormalCell(match[i].x, match[i].y))
                            {
                                needReFill = true;

                                if (match[i] == pressedCell || match[i] == enteredCell)
                                {
                                    specialCellX = match[i].x;
                                    specialCellY = match[i].y;
                                }
                            }
                        }

                        if (specialCellType != CellType.COUNT)
                        {
                            Destroy(cellsData[specialCellX, specialCellY]);
                            CellBase newCell = SpawnNewGameCell(specialCellX, specialCellY, specialCellType, colorType);
                            if ((specialCellType == CellType.FOUR_ROW_BONUS || specialCellType == CellType.FOUR_COLOM_BONUS) && newCell.IsColored()/* && match[0].IsColored()*/)
                            {
                                /* newCell.colorGameCellComponent.SetColor(match[0].colorGameCellComponent.Color);*/
                                newCell.colorGameCellComponent.SetColor(ColorGameCell.ColorType.ANY);
                            }
                            else if (specialCellType == CellType.ANY_COLOR_BONUS && newCell.IsColored())
                            {
                                newCell.colorGameCellComponent.SetColor(ColorGameCell.ColorType.ANY);
                            }
                        }
                    }
                }
            }
        }
        return needReFill;
    }
    public List<CellBase> GetMatch(CellBase cell, int newX, int newY)
    {
        if (cell.IsColored())
        {
            ColorGameCell.ColorType color = cell.colorGameCellComponent.Color;
            Debug.Log($"Init Color {color}, Init type {cell.cellType}");
            List<CellBase> horizontalCells = new List<CellBase>();
            List<CellBase> verticalCells = new List<CellBase>();
            List<CellBase> matchingCells = new List<CellBase>();


            #region check Horizontal
            horizontalCells.Add(cell);
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int xOffset = 1; xOffset < xDimension; xOffset++)
                {
                    int x;
                    if (direction == 0)
                    {
                        //left
                        x = newX - xOffset;
                    }
                    else
                    {// right
                        x = newX + xOffset;
                    }
                    if (x < 0 || x >= xDimension)
                    {
                        break;
                    }
                    if (cellsData[x, newY].IsColored() && cellsData[x, newY].colorGameCellComponent.Color == color)
                    {
                        horizontalCells.Add(cellsData[x, newY]);
                    }
                    else
                    {
                        break;
                    }
                    if (cellsData[x, newY].IsColored() && cellsData[x, newY].colorGameCellComponent.Color != ColorGameCell.ColorType.YELLOW)
                        Debug.Log($"color founded {cellsData[x, newY].colorGameCellComponent.Color} == {color}. pos: {x},{newY}.");
                }

            }

            if (horizontalCells.Count >= 3)
            {
                for (int i = 0; i < horizontalCells.Count; i++)
                {
                    matchingCells.Add(horizontalCells[i]);
                }
            }

            //serch vertical lines(for L and T figure)

            if (horizontalCells.Count >= 3)
            {
                for (int i = 0; i < horizontalCells.Count; i++)
                {
                    for (int direction = 0; direction <= 1; direction++)
                    {
                        for (int yOffset = 1; yOffset < yDimension; yOffset++)
                        {
                            int y;
                            if (direction == 0)
                            {//Up
                                y = newY - yOffset;
                            }
                            else
                            {
                                //Down
                                y = newY + yOffset;
                            }
                            if (y < 0 || y >= yDimension)
                            {
                                break;
                            }

                            if (cellsData[horizontalCells[i].x, y].IsColored() && cellsData[horizontalCells[i].x, y].colorGameCellComponent.Color == color)
                            {
                                verticalCells.Add(cellsData[horizontalCells[i].x, y]);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (verticalCells.Count < 2)
                    {
                        verticalCells.Clear();
                    }
                    else
                    {
                        for (int j = 0; j < verticalCells.Count; j++)
                        {
                            matchingCells.Add(verticalCells[j]);
                        }
                        break;
                    }
                }
            }



            if (matchingCells.Count >= 3)
            {
                return matchingCells;
            }
            #endregion

            #region check Vertical

            horizontalCells.Clear();
            verticalCells.Clear();

            verticalCells.Add(cell);
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int yOffset = 1; yOffset < yDimension; yOffset++)
                {
                    int y;
                    if (direction == 0)
                    {
                        //Up
                        y = newY - yOffset;
                    }
                    else
                    {
                        // Down
                        y = newY + yOffset;
                    }
                    if (y < 0 || y >= yDimension)
                    {
                        break;
                    }

                    if (cellsData[newX, y].IsColored() && cellsData[newX, y].colorGameCellComponent.Color == color)
                    {
                        verticalCells.Add(cellsData[newX, y]);
                    }
                    else
                    {
                        break;
                    }

                }

            }

            if (verticalCells.Count >= 3)
            {
                for (int i = 0; i < verticalCells.Count; i++)
                {
                    matchingCells.Add(verticalCells[i]);
                }
            }

            //serch horizontal lines(for L and T figure)

            if (verticalCells.Count >= 3)
            {
                for (int i = 0; i < verticalCells.Count; i++)
                {
                    for (int direction = 0; direction <= 1; direction++)
                    {
                        for (int xOffset = 1; xOffset < xDimension; xOffset++)
                        {
                            int x;
                            if (direction == 0)
                            {//Left
                                x = newX - xOffset;
                            }
                            else
                            {
                                //Right
                                x = newX + xOffset;
                            }
                            if (x < 0 || x >= xDimension)
                            {
                                break;
                            }

                            if (cellsData[x, verticalCells[i].y].IsColored() && cellsData[x, verticalCells[i].y].colorGameCellComponent.Color == color)
                            {
                                horizontalCells.Add(cellsData[x, verticalCells[i].y]);
                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                    if (horizontalCells.Count < 2)
                    {
                        horizontalCells.Clear();
                    }
                    else
                    {
                        for (int j = 0; j < horizontalCells.Count; j++)
                        {
                            matchingCells.Add(horizontalCells[j]);
                        }
                        break;
                    }
                }
            }


            if (matchingCells.Count >= 3)
            {
                return matchingCells;
            }
            #endregion

        }
        return null;
    }

    public void ClearObstacles(int x, int y)
    {
        for (int adjacentX = x - 1; adjacentX <= x + 1; adjacentX++)
        {
            if (adjacentX != x && adjacentX >= 0 && adjacentX < xDimension)
            {
                if (cellsData[adjacentX, y].cellType == CellType.OBSTACLE && cellsData[adjacentX, y].IsClearadle())
                {
                    cellsData[adjacentX, y].clearableGameCellComponent.Clear();
                    SpawnNewGameCell(adjacentX, y, CellType.EMPTY, ColorGameCell.ColorType.GREEN);
                }
            }
        }

        for (int adjacentY = y - 1; adjacentY <= y + 1; adjacentY++)
        {
            if (adjacentY != y && adjacentY >= 0 && adjacentY < yDimension)
            {
                if (cellsData[x, adjacentY].cellType == CellType.OBSTACLE && cellsData[x, adjacentY].IsClearadle())
                {
                    cellsData[x, adjacentY].clearableGameCellComponent.Clear();
                    SpawnNewGameCell(x, adjacentY, CellType.EMPTY, ColorGameCell.ColorType.GREEN);
                }
            }
        }
    }

    public void ClearRow(int row)
    {
        for (int x = 0; x < xDimension; x++)
        {
            ClearNormalCell(x, row);
        }
    }
    public void ClearColomn(int colomn)
    {
        for (int y = 0; y < yDimension; y++)
        {
            ClearNormalCell(colomn, y);
        }
    }

    public void ClearColor(ColorGameCell.ColorType color)
    {
        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                if (cellsData[x, y].IsColored() && cellsData[x, y].colorGameCellComponent.Color == color || color == ColorGameCell.ColorType.ANY)
                {
                    ClearNormalCell(x, y);
                }
            }
        }
    }

    public List<CellBase> GetCellsOfType(CellType type)
    {
        List<CellBase> cellsOftype = new List<CellBase>();


        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                if (cellsData[x, y].cellType == type)
                {
                    cellsOftype.Add(cellsData[x, y]);
                }
            }
        }

        return cellsOftype;
    }
    public void GameOver()
    {
        gameOver = true;
    }


    public enum CellType
    {
        EMPTY,
        Red,
        Purple,
        Yellow,
        OBSTACLE,
        FOUR_ROW_BONUS,
        FOUR_COLOM_BONUS,
        ANY_COLOR_BONUS,
        COUNT,
        NORMAL,
    }

    [System.Serializable]
    public struct CellToPrefab
    {
        public CellType type;
        public CellBase prefab;
    }
    [System.Serializable]
    public struct InitialCellPosition
    {
        public CellType type;
        public int x;
        public int y;
        public ColorGameCell.ColorType color;
    }


    private void OnDrawGizmos()
    {

    }

}
