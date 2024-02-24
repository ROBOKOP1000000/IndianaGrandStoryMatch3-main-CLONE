using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Analytics;
public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    [SerializeField] private Text textMoney;
    [SerializeField] private Text textMoneyShop;
    [SerializeField] private Text textLvl;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject lives;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject preLvl;
    private void Update()
    {
        textMoney.text = PlayerPrefs.GetInt("Money").ToString();
        textMoneyShop.text = PlayerPrefs.GetInt("Money").ToString();
    }
    private void Awake()
    {
        textLvl.text = PlayerPrefs.GetInt("CurrentLvl").ToString();
        instance = this;
    }
    public void OpenOptions()
    {
        panel.SetActive(true);
        options.SetActive(true);
    }
    public void CloseOptions()
    {
        StartCoroutine(TimerClose(options, 0.7f));
    }
    public void OpenPreLvl()
    {
        PreBoost.isCoin = false;
        PreBoost.isBomb = false;
        PreBoost.isSpark = false;
        PreBoost.isPlane = false;
        preLvl.SetActive(true);
        panel.SetActive(true);
    }
    public void ClosePreLvl()
    {
        StartCoroutine(TimerClose(preLvl, 0.7f));
    }
    public void OpenLives()
    {
        if (PlayerPrefs.HasKey("Hearts"))
        {
            if (PlayerPrefs.GetInt("Hearts") < 5)
                panel.SetActive(true);
                lives.SetActive(true);

            // Добавьте следующую строку для вызова события аналитики
            if (PlayerPrefs.GetInt("Hearts") == 0)
            {
                FirebaseManager.Instance.LogAnalyticsEvent("First_run_life_zero_001");
                Debug.Log("Firebase event First_run_life_zero_001 logged successfully.");
            }
        }
    }
    public void CloseLives()
    {
        StartCoroutine(TimerClose(lives, 0.7f));
    }
    IEnumerator TimerClose(GameObject board, float time)
    {
        board.GetComponent<Animator>().SetBool("isClose", true);
        panel.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        board.SetActive(false);
        panel.SetActive(false);
    }
}
