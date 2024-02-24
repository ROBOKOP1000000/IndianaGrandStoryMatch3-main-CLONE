using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Analytics
{
    public class FirebaseManager : MonoBehaviour
    {
        private static FirebaseManager _instance;
        public static FirebaseManager Instance => _instance;

        private bool _canUseAnalytics = false;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // РРЅРёС†РёР°Р»РёР·Р°С†РёСЏ Analytics
                    Debug.Log("Firebase Analytics initialized successfully!");
                    _canUseAnalytics = true;
                }
                else
                {
                    Debug.LogError(string.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // РћР±СЂР°Р±РѕС‚РєР° РѕС€РёР±РєРё - Firebase РЅРµ РґРѕСЃС‚СѓРїРµРЅ
                }
            });
        }

        public void FirstLevelOpen()
        {
            // Проверка флага, указывающего, можно ли использовать аналитику
            if (!_canUseAnalytics)
                return;

            // Логирование события аналитики Firebase - первый запуск предуровня
            FirebaseAnalytics.LogEvent("First_run_level_run_001");
        }


        public void FirstClickAdvertising ()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_adv_open_001");
        }

        public void FirstRunMoneyBuyLife()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_money_buy_life_001");
        }

        public void FirstRunAdvBuyLife()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_adv_buy_life_002");
        }

        public void FirstRunToolActivate()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_tool_activate_001");
        }

        public void FirstRunToolUse()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_tool_use_001");
        }

        public void FirstRunLevelLose ()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_level_lose_001");
        }

        public void FirstRunLevelMoneyRestart()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_level_money_restart_001");
        }

        public void FirstRunLevelAdvRestart()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_level_adv_restart_001");
        }


        public void FirstRunLoseLife()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_lose_life_001");
        }


        public void FirstRunLifeZero()
        {
            if (!_canUseAnalytics)
                return;

            FirebaseAnalytics.LogEvent("First_run_life_zero_001");
        }
        

        public void LogAnalyticsEvent(string eventName)
        {

            if (!_canUseAnalytics)
                return;

    
            FirebaseAnalytics.LogEvent(eventName);
        }
    }
}



