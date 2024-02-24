using Analytics;
using Gamr.Monetization.Ads;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    private AdmobInterstitialAd _interstitialAd;
    private static bool _isFirstLaunch = true;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CurrentLvl"))
        {
            PlayerPrefs.SetInt("CurrentLvl", 1);
        }

        // ������������� �������
        _interstitialAd = gameObject.AddComponent<AdmobInterstitialAd>();
        _interstitialAd.InitializeAd();

        // ���������, ������ �� ��� ������
        if (_isFirstLaunch)
        {
            _isFirstLaunch = false;
            // ��������� ������� ������ ��� ������ �������
            _interstitialAd.LoadAndShowAd();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        FirebaseManager.Instance.FirstRunLoseLife();
    }

    public void LvlStart()
    {
        if (PlayerPrefs.GetInt("Hearts") > 0)
        {

            // ��������� �������
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLvl") + "Lvl");
        }
        else
        {
            // ��������� ���� ������
            MainMenuManager.instance.OpenLives();
        }
    }
}












