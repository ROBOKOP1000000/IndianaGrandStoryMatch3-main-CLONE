using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamr.Monetization.Ads
{
    public class AdmobInterstitialAd : MonoBehaviour
    {
        private string _unitId;          // Идентификатор рекламного блока
        private InterstitialAd _ad;      // Экземпляр межстраничного объявления

        // Метод для инициализации рекламы с использованием идентификатора
        public void InitializeAd()
        {
            _unitId = "ca - app - pub - 3940256099942544 / 1033173712";
        }

        // Метод для загрузки и отображения межстраничного объявления
        public void LoadAndShowAd()
        {
            // Проверяем, если объявление уже существует, уничтожаем его
            // предотвращает появление одной и той же рекламы
            if (_ad != null)
            {
                _ad.Destroy();
                _ad = null;
            }

            // Создаем запрос на загрузку объявления
            var req = new AdRequest();

            // Загружаем объявление с использованием идентификатора и запроса
            InterstitialAd.Load(_unitId, req, (ad, error) =>
            {
                // Проверяем наличие ошибок при загрузке
                if (error != null || ad == null)
                {
                    // Логика обработки ошибки, если нужно
                    Debug.LogError($"Ad loading failed: {error}");
                    return;
                }

                // Присваиваем загруженное объявление экземпляру класса
                _ad = ad;

                // Показываем объявление, если оно загружено и готово к показу
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




