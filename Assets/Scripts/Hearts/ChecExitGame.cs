using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecExitGame : MonoBehaviour
{
    private void OnApplicationQuit()
    {
    }
    private bool hasFocus = true;


    private void OnApplicationPause(bool pause)
    {
        if (hasFocus && !pause)
        {
            // Приложение только что получило фокус после того, как было свернуто
            // выполнить определенные действия здесь
            PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") + 1);
        }
        else if (pause)
        {

            PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") - 1);
        }

        hasFocus = pause;
    }

}
