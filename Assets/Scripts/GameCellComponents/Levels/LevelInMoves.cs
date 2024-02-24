using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelInMoves : LevelBase
{
    [SerializeField] private Text textMoves;
    [SerializeField] private Text textCurrent;
    public int moveNumber;
    public int targetScore;
    private int moveUsed = 0;

    private void Start()
    {
        type = LevelType.CLEAR_IN_MOVES;
        Debug.Log("Number of moves:" + moveNumber + "target score:" + targetScore);
    }



    public override void OnMove()
    {
        base.OnMove();
        moveUsed++;


        if(moveNumber-moveUsed == 0)
        {
            if(currentScore>= targetScore)
            {
                GameWin();
            }
            else
            {
                GameLose();
            }
        }
        textMoves.text = "Moves All" + moveNumber;
        textCurrent.text = "Moves Current" + moveUsed;
    }
}
