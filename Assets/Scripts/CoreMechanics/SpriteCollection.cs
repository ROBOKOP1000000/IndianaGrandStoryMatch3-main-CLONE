using System;
using UnityEngine;

public class SpriteCollection : MonoBehaviour
{
    public static SpriteCollection Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    internal void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    public Sprite GemRed;
    public Sprite GemGreen;
    public Sprite GemBlue;
    public Sprite GemYellow;
    public Sprite GemPink;

    public Sprite BoosterSparkHorisontal;
    public Sprite BoosterSparkVertical;
    public Sprite BoosterPlane;
    public Sprite BoosterBomb;
    public Sprite BoosterCoin;

    public Sprite Rock;

    public Sprite CatRed;
    public Sprite CatGreen;
    public Sprite CatBlue;
    public Sprite CatYellow;
    public Sprite CatPink;

    public Sprite BoxUnharmed;
    public Sprite BoxDamaged;
    public Sprite BoxBroken;

    public Sprite IceUnharmed;
    public Sprite IceDamaged;
}