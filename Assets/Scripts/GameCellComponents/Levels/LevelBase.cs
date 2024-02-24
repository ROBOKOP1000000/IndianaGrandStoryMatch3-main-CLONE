using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    public LevelType type;
    public GridManager grid;

    public int score_1_Star;
    public int score_2_Star;
    public int score_3_Star;

    public float currentScore { get; private set; }

    public virtual void GameWin()
    {
        Debug.Log("Win");
        grid.GameOver();
    }

    public virtual void GameLose()
    {
        Debug.Log("Lose");

        grid.GameOver();
    }

    public virtual void OnMove()
    {

        Debug.Log("Move");
    }

    public virtual void OnCellCleared(CellBase cell)
    {
        currentScore += cell.score;
    }



    public enum LevelType
    {
        CLEAR_IN_TIME,
        CLEAR_OBSTACLE,
        CLEAR_IN_MOVES,
    }

}
