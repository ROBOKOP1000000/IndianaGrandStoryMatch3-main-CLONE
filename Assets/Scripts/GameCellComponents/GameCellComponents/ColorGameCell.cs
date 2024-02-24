using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorGameCell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public ColorToSprite[] colorToSprite;
    private Dictionary<ColorType, Sprite> colorToSpriteDictionary;
    private ColorType color;
    public ColorType Color
    {
        get { return color; }
        set { SetColor(value); }

    }

    public int NumColors
    {
        get { return colorToSprite.Length; }
    }

    public void SetColor(ColorType newColor)
    {
        color = newColor;

        if (colorToSpriteDictionary.ContainsKey(newColor))
        {
            spriteRenderer.sprite = colorToSpriteDictionary[newColor];
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorToSpriteDictionary = new Dictionary<ColorType, Sprite>();

        for (int i = 0; i < colorToSprite.Length; i++)
        {
            if (!colorToSpriteDictionary.ContainsKey(colorToSprite[i].color))
            {
                colorToSpriteDictionary.Add(colorToSprite[i].color, colorToSprite[i].sprite);
            }
        }
    }


    public enum ColorType
    {
        YELLOW,
        PURPLE,
        RED,
        BLUE,
        GREEN,
        ANY,
        COUNT,
    }

    [System.Serializable]
    public struct ColorToSprite
    {
        public ColorType color;
        public Sprite sprite;
    }
}
