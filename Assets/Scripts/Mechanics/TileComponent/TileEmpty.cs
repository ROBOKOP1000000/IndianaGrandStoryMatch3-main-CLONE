//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TileEmpty : ITile
//{
//    public int X { get; set; }
//    public int Y { get; set; }

//    private TileType _tileType;
//    public Image Icon { get; set; }
//    public Button TileButton { get; set; }

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

//    public TileEmpty()
//    {
//        _tileType = TileType.Empty;
//    }

//    public TileEmpty(Image icon)
//    {
//        _tileType = TileType.Empty;
//        Icon = icon;
//        Icon.color= Color.clear;
//    }
//}
