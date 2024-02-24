using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamr.Monetization.Ads
{
    public class AdmobInterstitialAd : MonoBehaviour
    {
        private string _unitId;          // ������������� ���������� �����
        private InterstitialAd _ad;      // ��������� �������������� ����������

        // ����� ��� ������������� ������� � �������������� ��������������
        public void InitializeAd()
        {
            _unitId = "ca - app - pub - 3940256099942544 / 1033173712";
        }

        // ����� ��� �������� � ����������� �������������� ����������
        public void LoadAndShowAd()
        {
            // ���������, ���� ���������� ��� ����������, ���������� ���
            // ������������� ��������� ����� � ��� �� �������
            if (_ad != null)
            {
                _ad.Destroy();
                _ad = null;
            }

            // ������� ������ �� �������� ����������
            var req = new AdRequest();

            // ��������� ���������� � �������������� �������������� � �������
            InterstitialAd.Load(_unitId, req, (ad, error) =>
            {
                // ��������� ������� ������ ��� ��������
                if (error != null || ad == null)
                {
                    // ������ ��������� ������, ���� �����
                    Debug.LogError($"Ad loading failed: {error}");
                    return;
                }

                // ����������� ����������� ���������� ���������� ������
                _ad = ad;

                // ���������� ����������, ���� ��� ��������� � ������ � ������
                if (_ad.CanShowAd())
                {
                    _ad.Show();
                }
            });
        }
    }
}




/*public void ShowInterstitial()
{
    if (fbParamInterstitialFrequency == 0)
    {
        Debug.Log("Interstitial disabled");
        return;
    }

    if (frequencyCount == fbParamInterstitialFrequency)
    {
        LoadInterstitialAd();
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
        frequencyCount = 1;
    }
    else
    {
        frequencyCount++;
    }
}*/




