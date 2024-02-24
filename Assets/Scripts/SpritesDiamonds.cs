using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesDiamonds : MonoBehaviour
{
    public static SpritesDiamonds Instance;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public Sprite yellow;
    public Sprite purple;
    private void Awake() => Instance = this;
}
