using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentLvl : MonoBehaviour
{
    [SerializeField] private Text lvlText;

    [Space]
    [SerializeField] private bool isLvlStartLooping;

    private const string preLvlText = "Lvl:";

    public static bool isFirstCheck = true;
    public static int currentLvl;

    private void Start()
    {
        if (isLvlStartLooping && isFirstCheck)
        {
            currentLvl = 16;
            isFirstCheck = false;
        }
        lvlText.text = preLvlText + currentLvl;
    }
    public void SetCurrentLvl()
    {
        currentLvl++;
    }
}
