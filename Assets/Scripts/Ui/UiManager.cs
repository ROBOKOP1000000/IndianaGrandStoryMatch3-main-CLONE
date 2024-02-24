using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Analytics;
public class UiManager : MonoBehaviour
{
    [SerializeField] private int moneyAdd;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject iconWin;
    [SerializeField] private Text textMoney;
    [SerializeField] private Text textTimerResume;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject watchButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject lose;
    [SerializeField] private GameObject afterLose;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject loseIcon;
    [SerializeField] private GameObject options;
    private bool isLoseMenu;
    private void Awake()
    {
        textMoney.text = PlayerPrefs.GetInt("Money").ToString();
    }
    IEnumerator TimerResume()
    {
        int i = 5;
        textTimerResume.text = i + "s";
        while (i != 0)
        {
            textTimerResume.text = i + "s";
            yield return new WaitForSeconds(1);
            i--;
        }
        yield return new WaitForEndOfFrame();
        watchButton.GetComponent<Animator>().SetBool("isGo", true);
        buyButton.SetActive(false);

    }
    public void OpenWin()
    {
        GameManager.instance.isGameOver = true;
        PlayerPrefs.SetInt("CurrentLvl", PlayerPrefs.GetInt("CurrentLvl") + 1);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + moneyAdd);
        panel.SetActive(true);
        win.SetActive(true);
        if (Complete5Lvl.instance != null)
        {
            Complete5Lvl.instance.Complete();
        }
        iconWin.SetActive(true);
    }
    public void ExitLvl()
    {
        if (PlayerPrefs.GetInt("Hearts") > 5)
        {
            PlayerPrefs.SetInt("Hearts", 5);
        }
        PlayerPrefs.SetString("TimeRecoveryHeart", DateTime.Now.ToString());
        PlayerPrefs.SetInt("Hearts", PlayerPrefs.GetInt("Hearts") - 1);
    }
    #region AfterLose
    public void OpenAfterLose(bool isLose)
    {
        isLoseMenu = isLose;
        afterLose.SetActive(true);
        panel.SetActive(true);
    }

    IEnumerator OpenAfterLoseTimer(bool count)
    {
        yield return new WaitForSeconds(1f);
        OpenAfterLose(count);
    }
    public void CloseAfterLose()
    {
        StartCoroutine(TimerCloseAfterLose(afterLose, 0.7f));
        if (isLoseMenu)
        {
            Invoke("OpenLose", 0.7f);
        }
        else
        {
            Invoke("OpenOptions", 0.7f);
        }
    }
    IEnumerator TimerCloseAfterLose(GameObject board, float time)
    {
        board.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        board.SetActive(false);
    }
    #endregion
    #region Options
    public void ExitViaOptions()
    {
        StartCoroutine(TimerCloseOptions(options, 0.8f));
        StartCoroutine(OpenAfterLoseTimer(false));
    }
    public void CloseOptions()
    {
        StartCoroutine(TimerClose(options, 0.8f));
    }
    IEnumerator TimerCloseOptions(GameObject board, float time)
    {
        board.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        board.SetActive(false);
    }
    public void OpenOptions()
    {
        panel.SetActive(true);
        options.SetActive(true);
    }
    #endregion
    #region Lose
    public void OpenLose()
    {
        textMoney.text = PlayerPrefs.GetInt("Money").ToString();
        //watchButton.GetComponent<Animator>().Rebind();
        //watchButton.GetComponent<Animator>().Update(0f);
        buyButton.SetActive(true);
        panel.SetActive(true);
        lose.SetActive(true);
        money.SetActive(true);
        GameManager.instance.isGameOver = true;
        loseIcon.SetActive(true);
        StartCoroutine(TimerResume());
        FirebaseManager.Instance.FirstRunLevelLose();
        Debug.Log("Firebase event First_run_level_lose_001 logged successfully.");
    }
    public void CloseLose()
    {
        StopAllCoroutines();
        StartCoroutine(TimerLoseClose(money, lose, loseIcon, 0.7f));
        StartCoroutine(OpenAfterLoseTimer(true));
    }
    public void BuyResume()
    {
        GameManager.instance.isGameOver = false;
        StopAllCoroutines();
        StartCoroutine(TimerResume(money, lose, loseIcon, 0.7f));
    }
    #endregion
    IEnumerator TimerClose(GameObject board, float time)
    {
        board.GetComponent<Animator>().SetBool("isClose", true);
        panel.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        board.SetActive(false);
        panel.SetActive(false);
    }
    IEnumerator TimerLoseClose(GameObject money, GameObject lose, GameObject icon, float time)
    {
        money.GetComponent<Animator>().SetBool("isClose", true);
        lose.GetComponent<Animator>().SetBool("isClose", true);
        //   panel.GetComponent<Animator>().SetBool("isClose", true);
        icon.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        money.SetActive(false);
        lose.SetActive(false);
        //  panel.SetActive(false);
        icon.SetActive(false);

    }
    IEnumerator TimerResume(GameObject money, GameObject lose, GameObject icon, float time)
    {
        money.GetComponent<Animator>().SetBool("isClose", true);
        lose.GetComponent<Animator>().SetBool("isClose", true);
        panel.GetComponent<Animator>().SetBool("isClose", true);
        icon.GetComponent<Animator>().SetBool("isClose", true);
        yield return new WaitForSeconds(time);
        money.SetActive(false);
        lose.SetActive(false);
        panel.SetActive(false);
        icon.SetActive(false);

    }
}
