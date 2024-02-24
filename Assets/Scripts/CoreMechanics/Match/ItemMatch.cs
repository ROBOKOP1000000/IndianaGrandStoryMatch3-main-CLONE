public sealed class ItemMatch
{
    public readonly int TypeId;

    public readonly int Score;

    public readonly Cell[] Tiles;

    public readonly ItemMatchType Type;

    public ItemMatch(Cell origin, Cell[] horizontal, Cell[] vertical, Cell[] square)
    {
        Type = ItemMatchType.Standart;

        TypeId = (int)origin.ItemType;
        if (horizontal.Length >= 2 && vertical.Length >= 2)
        {
            Type = ItemMatchType.FiveFigure;

            Tiles = new Cell[horizontal.Length + vertical.Length + 1];

            Tiles[0] = origin;

            //for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //{
            //    if (GameManager.instance.tileTypeNeeds[i] == horizontal[0].Type)
            //    {

            //        GameManager.instance.countNeed[i] -= 1;
            //    }

            //}

            //g

            horizontal.CopyTo(Tiles, 1);

            vertical.CopyTo(Tiles, horizontal.Length + 1);
        }
        else if (square.Length >= 1)
        {
            //for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //{
            //    if (GameManager.instance.tileTypeNeeds[i] == horizontal[0].Type)
            //    {
            //        GameManager.instance.countNeed[i] -= 1;
            //    }

            //}
            Type = ItemMatchType.Square;

            Tiles = new Cell[square.Length + 1];

            Tiles[0] = origin;

            square.CopyTo(Tiles, 1);
        }
        else if (horizontal.Length >= 2)
        {
            if (horizontal.Length == 3)
            {
                Type = ItemMatchType.FourHorisontal;
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == horizontal[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

             //   }
            }
            else if (horizontal.Length == 4)
            {
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == horizontal[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

            //    }
                Type = ItemMatchType.FiveRow;
            }
            if (horizontal.Length == 2)
            {
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == horizontal[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

            //    }
            }

            //Debug.Log($"Ok horizontal origin:{origin.X} {origin.Y}\nhorizontal{horizontal.Length} \nvertical{vertical.Length}");

            Tiles = new Cell[horizontal.Length + 1];

            Tiles[0] = origin;

            horizontal.CopyTo(Tiles, 1);
        }
        else if (vertical.Length >= 2)
        {
            if (vertical.Length == 3)
            {
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == vertical[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

               // }
                Type = ItemMatchType.FourVertical;
            }
            else if (vertical.Length == 4)
            {
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == vertical[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

            //    }
                Type = ItemMatchType.FiveRow;
            }
            if (vertical.Length == 2)
            {
            //    for (int i = 0; i < GameManager.instance.tileTypeNeeds.Length; i++)
            //    {
            //        if (GameManager.instance.tileTypeNeeds[i] == vertical[0].Type)
            //        {
            //            GameManager.instance.countNeed[i] -= 1;
            //        }

            //    }
            }
            Tiles = new Cell[vertical.Length + 1];

            Tiles[0] = origin;

            vertical.CopyTo(Tiles, 1);
        }
        else Tiles = null;

        Score = Tiles?.Length ?? -1;
    }
}