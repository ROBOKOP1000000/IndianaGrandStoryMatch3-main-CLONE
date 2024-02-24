using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HeartController : MonoBehaviour
{
    [SerializeField] private int timeAddHeart;

    [SerializeField] private Text heartText;

    private const string heartsKey = "Hearts";
    private const string moneyKey = "Money";
    private const string TimeRecoveryHeartKey = "TimeRecoveryHeart";
    private void Start()
    {
        if (!PlayerPrefs.HasKey(heartsKey) || PlayerPrefs.GetInt(heartsKey) > 5)
        {
            PlayerPrefs.SetInt(heartsKey, 5);
        }
    }
    public void BuyHearts()
    {
        if (PlayerPrefs.HasKey(moneyKey) && PlayerPrefs.GetInt(moneyKey) >= 250)
        {
            PlayerPrefs.SetInt(moneyKey, PlayerPrefs.GetInt(moneyKey) - 250); ;
            PlayerPrefs.SetInt(heartsKey, 5);
            PlayerPrefs.SetString(TimeRecoveryHeartKey, DateTime.Now.ToString());
            MainMenuManager.instance.CloseLives();
        }
    }
    private void Update()
    {
        AddHearts();
        if (!PlayerPrefs.HasKey(heartsKey))
        {
            PlayerPrefs.SetInt(heartsKey, 5);
        }
        heartText.text = PlayerPrefs.GetInt(heartsKey).ToString();
    }
    void AddHearts()
    {
        if (PlayerPrefs.HasKey(TimeRecoveryHeartKey) && PlayerPrefs.GetInt(heartsKey) < 5)
        {
            print(PlayerPrefs.GetString(TimeRecoveryHeartKey));
            TimeSpan ts;
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString(TimeRecoveryHeartKey));
            if (ts.TotalMinutes >= timeAddHeart)
            {
                for (int i = timeAddHeart; i < timeAddHeart * 6; i += timeAddHeart)
                {
                    if (ts.TotalMinutes > i)
                    {
                        PlayerPrefs.SetInt(heartsKey, PlayerPrefs.GetInt(heartsKey) + 1);
                    }
                    else
                    {
                        PlayerPrefs.SetString(TimeRecoveryHeartKey, DateTime.Now.ToString());
                    }
                    if (i == 6)
                    {
                        PlayerPrefs.SetString(TimeRecoveryHeartKey, DateTime.Now.ToString());
                    }
                }
            }


        }
    }


}
