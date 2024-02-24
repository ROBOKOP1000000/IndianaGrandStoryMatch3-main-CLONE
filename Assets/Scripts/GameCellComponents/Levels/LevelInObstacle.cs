using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInObstacle : LevelBase
{
    public int numberMoves;
    public GridManager.CellType[] obstacleType;

    private int moveUsed = 0;
    private int numberObstacleLeft;


    private void Start()
    {
        type = LevelType.CLEAR_OBSTACLE;


        for (int i = 0; i < obstacleType.Length; i++)
        {
            numberObstacleLeft += grid.GetCellsOfType(obstacleType[i]).Count;
        }
    }



    public override void OnMove()
    {
        base.OnMove();

        moveUsed++;
        if(numberMoves - moveUsed == 0 && numberObstacleLeft>0)
        {
            GameLose();
        }
    }

    public override void OnCellCleared(CellBase cell)
    {
        base.OnCellCleared(cell);

        for (int i = 0; i < obstacleType.Length; i++)
        {
            if(obstacleType[i] == cell.cellType)
            {
                numberObstacleLeft--;

                if (numberObstacleLeft <= 0)
                {
                    GameWin();
                }
            }
        }
    }
}
