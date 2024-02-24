using System.Text.RegularExpressions;
using UnityEngine;

public class PlayableBoard : MonoBehaviour
{
    [Header("Playable field:\n1 - playable\n0 - non playable")]
    [TextArea(3, 10)]
    public string Field;

    public CellType[,] CellTypes;

    [Header("Gem position:\n" +
        "r - Red\n" +
        "g - Green\n" +
        "b - Blue\n" +
        "y - Yellow\n" +
        "p - Pink\n" +
        "s - Stone\n" +
        "t - Tile")]
    [TextArea(3, 10)]
    public string Gems;

    public GemColor[,] GemColors;
    public ItemType[,] ItemTypes;
    public ObstacleType[,] ObstacleTypes;

    private readonly char RED = 'r';
    private readonly char GREEN = 'g';
    private readonly char BLUE = 'b';
    private readonly char YELLOW = 'y';
    private readonly char PINK = 'p';

    private readonly char STONE = 's';
    private readonly char TILE = 't';

    public void Initialize(int columns, int rows)
    {
        InitCellTypes(columns, rows);
        InitGemColors(columns, rows);
    }

    private void InitGemColors(int columns, int rows)
    {
        GemColors = new GemColor[columns, rows];
        ItemTypes = new ItemType[columns, rows];
        ObstacleTypes = new ObstacleType[columns, rows];

        var str = Regex.Replace(Gems, @"[ \r\n\t]", "");
        //str = str.ToLower();

        int column = 0;
        int row = rows - 1;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == RED)
            {
                GemColors[column, row] = GemColor.Red;
                ItemTypes[column, row] = ItemType.Gem;
            }
            if (str[i] == GREEN)
            {
                GemColors[column, row] = GemColor.Green;
                ItemTypes[column, row] = ItemType.Gem;
            }
            if (str[i] == BLUE)
            {
                GemColors[column, row] = GemColor.Blue;
                ItemTypes[column, row] = ItemType.Gem;
            }
            if (str[i] == YELLOW)
            {
                GemColors[column, row] = GemColor.Yellow;
                ItemTypes[column, row] = ItemType.Gem;
            }
            if (str[i] == PINK)
            {
                GemColors[column, row] = GemColor.Pink;
                ItemTypes[column, row] = ItemType.Gem;
            }
            if (str[i] == STONE)
            {
                ObstacleTypes[column, row] = ObstacleType.Stone;
                ItemTypes[column, row] = ItemType.Dynamic;
            }
            if (str[i] == TILE)
            {
                ObstacleTypes[column, row] = ObstacleType.Tile;
                ItemTypes[column, row] = ItemType.Static;
            }

            if (column < columns - 1)
            {
                column++;
            }
            else
            {
                column = 0;
                if (row > 0)
                {
                    row--;
                }
            }
        }
    }

    private void InitCellTypes(int columns, int rows)
    {
        CellTypes = new CellType[columns, rows];

        var str = Regex.Replace(Field, @"[ \r\n\t]", "");

        int column = 0;
        int row = rows - 1;

        for (int i = 0; i < str.Length; i++)
        {
            if (int.TryParse(str[i].ToString(), out int result))
            {
                CellTypes[column, row] = (CellType)result;
            }

            if (column < columns - 1)
            {
                column++;
            }
            else
            {
                column = 0;
                if (row > 0)
                {
                    row--;
                }
            }
        }
    }
}