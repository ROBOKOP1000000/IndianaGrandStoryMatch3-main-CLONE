//using UnityEngine;
//using UnityEngine.UI;

//public class TileStone : ITile
//{
//    public int X { get; set; }
//    public int Y { get; set; }
//    public Image Icon { get; set; }
//    public Button TileButton { get; set; }

//    private TileType _tileType;

//    private ITile.SelectHandler? notify;

//    event ITile.SelectHandler ITile.Notify
//    {
//        add
//        {
//            //notify += value;
//        }

//        remove
//        {
//            //notify -= value;
//        }
//    }

//    public TileType Type { get => _tileType; set => _tileType = value; }

//    public void SetType(TileType type)
//    {
//    }

//    public TileStone(Image icon , Button button, TileType type)
//    {
//        Icon = icon;
//        TileButton = button;
//        _tileType = type;
//        Icon.color = Color.black;
//    }
//}