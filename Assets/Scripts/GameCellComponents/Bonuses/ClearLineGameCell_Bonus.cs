using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLineGameCell_Bonus : ClearableGameCell
{
    public bool isRow;


    public override void Clear()
    {
        base.Clear();

        if (isRow)
        {
            cell.grid.ClearRow(cell.y);
        }
        else
        {

            cell.grid.ClearColomn(cell.x);
        }
    }
}
