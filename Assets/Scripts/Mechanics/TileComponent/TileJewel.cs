//using UnityEngine;
//using UnityEngine.UI;

//public class TileJewel : ITile
//{
//    public int X { get; set; }
//    public int Y { get; set; }

//    private TileType _type;
//    public TileType Type
//    {
//        get { return _type; }
//        set
//        {
//            if (_type != value)
//            {
//                _type = value;
//                SwitchJewelView(value);
//            }
//        }
//    }

//    public Image Icon { get; set; }
//    public Button TileButton { get; set; }

//    private ITile.SelectHandler? notify;

//    event ITile.SelectHandler ITile.Notify
//    {
//        add
//        {
//            notify += value;
//        }

//        remove
//        {
//            notify -= value;
//        }
//    }
   
//    public TileJewel(Image icon, Button button, TileType type)
//    {
//        Type = type;
//        Icon = icon;
//        TileButton = button;
//        SwitchJewelView(type);
//    }

//    private void SwitchJewelView(TileType type)
//    {
//        if (Icon == null)
//        {
//            return;
//        }

//        switch (type)
//        {
//            case TileType.Red:
//                Icon.sprite = SpritesDiamonds.Instance.red;

//                break;

//            case TileType.Green:
//                Icon.sprite = SpritesDiamonds.Instance.green;

//                break;

//            case TileType.Blue:
//                Icon.sprite= SpritesDiamonds.Instance.blue;

//                break;

//            case TileType.Pink:
//                Icon.sprite = SpritesDiamonds.Instance.purple;

//                break;

//            case TileType.Yellow:
//                Icon.sprite = SpritesDiamonds.Instance.yellow;

//                break;
//        }
//    }
//}