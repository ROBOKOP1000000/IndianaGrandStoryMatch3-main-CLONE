using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheatManager : MonoBehaviour
{
    [SerializeField] private GameObject cheatPanel;

    public void SetMoney()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 150);
    }
    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenCheat()
    {
        cheatPanel.SetActive(true);
    }
    public void CloseCheat()
    {
        cheatPanel?.SetActive(false);
    }
}
