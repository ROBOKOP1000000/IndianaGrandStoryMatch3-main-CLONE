using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete5Lvl : MonoBehaviour
{
    public static Complete5Lvl instance;
    public void Complete()
    {
        PlayerPrefs.SetString("isOffer5Day", "true");
        PlayerPrefs.SetString("isSpecialOffer", "true");
    }
    private void Awake()
    {
        instance = this;
    }
}
