using Analytics;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gamr.Monetization.Ads;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private Text textMoves;
    [SerializeField] private Text[] textCount;
    public bool isColor;
    public bool isStone;
    public bool isAnubis;
    public static GameManager instance;
    public GemColor[] tileTypeNeeds;
    public ObstacleType[] obstacleTypeNeeds;
    public int[] countNeed;
    public int moves;
    private bool isFirst = true;
    private int checkCount;
    [SerializeField] private UiManager uiManager;
    private const string heartsKey = "Hearts";
    private const string TimeRecoveryHeartKey = "TimeRecoveryHeart";

    private FirebaseManager firebaseManager;

    private AdmobInterstitialAd interstitialAd;



    private void Awake()
    {
        isGameOver = false;
        checkCount = countNeed.Length;
        if (instance == null)
        {
            instance = this;
        }

        firebaseManager = FirebaseManager.Instance;
        if (firebaseManager == null)
        {
            Debug.LogError("FirebaseManager not found.");
        }

        // Инициализация рекламы
        interstitialAd = gameObject.AddComponent<AdmobInterstitialAd>();
        interstitialAd.InitializeAd();
    }

    private void Update()
    {
        textMoves.text = moves.ToString();
        for (int i = 0; i < countNeed.Length; i++)
        {
            if (countNeed[i] >= 0)
            {
                textCount[i].text = countNeed[i].ToString();
            }
            else
            {
                textCount[i].text = 0.ToString();
            }
        }

        if (moves <= 0 && isFirst)
        {
            // Показываем рекламу при поражении 
            interstitialAd.LoadAndShowAd();

            uiManager.OpenLose();
            isFirst = false;
        }

        for (int i = 0; i < countNeed.Length; i++)
        {
            if (countNeed[i] <= 0)
            {
                checkCount--;
            }
            if (i == countNeed.Length - 1)
            {
                if (checkCount <= 0 && isFirst)
                {
                        // Показываем рекламу при победе
                    interstitialAd.LoadAndShowAd();


                    uiManager.OpenWin();

                    if (firebaseManager != null)
                    {
                        firebaseManager.LogAnalyticsEvent($"First_run_level_win_{PlayerPrefs.GetInt("CurrentLvl") - 1}");
                        Debug.Log($"Level {PlayerPrefs.GetInt("CurrentLvl") - 1} won");
                    }
                    else
                    {
                        Debug.LogError("FirebaseManager is not initialized.");
                    }

                    isFirst = false;
                }
                else
                {
                    checkCount = countNeed.Length;
                }
            }
        }
    }

    public void ResumeGame()
    {
        if (PlayerPrefs.GetInt("Money") >= 100)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 100);
            uiManager.BuyResume();

            FirebaseManager.Instance.FirstRunLevelMoneyRestart();
            Debug.Log("ResumeGame: FirstRunLevelMoneyRestart analytics sent.");
        }
    }

    public void Lose()
    {
        isGameOver = true;
        if (PlayerPrefs.GetInt(heartsKey) > 0)
        {
            PlayerPrefs.SetInt(heartsKey, PlayerPrefs.GetInt(heartsKey) - 1);
        }
        PlayerPrefs.SetString(TimeRecoveryHeartKey, DateTime.Now.ToString());
    }
}
