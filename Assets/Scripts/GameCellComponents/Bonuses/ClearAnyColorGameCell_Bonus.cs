using UnityEngine;

public class ClearAnyColorGameCell_Bonus : ClearableGameCell
{
    [HideInInspector] public ColorGameCell.ColorType color;


    public override void Clear()
    {
        base.Clear();

        cell.grid.ClearColor(color);
    }

}
