using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyTest : MonoBehaviour
{
   public void Add()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 150);
    }
}
