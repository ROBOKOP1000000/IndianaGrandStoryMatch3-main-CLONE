using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Analytics;

public class PreBoost : MonoBehaviour
{
    public static bool isPlane;
    public static bool isBomb;
    public static bool isCoin;
    public static bool isSpark;

    private FirebaseManager firebaseManager;

    void Start()
    {
        firebaseManager = FirebaseManager.Instance;

        if (firebaseManager == null)
        {
            Debug.LogError("FirebaseManager not found.");
        }
    }

    public void SetPlane()
    {
        isPlane = true;
        LogAnalyticsEvent("First_run_bonuce_001");
    }

    public void SetBomb()
    {
        isBomb = true;
        LogAnalyticsEvent("First_run_bonuce_001");
    }

    public void SetCoin()
    {
        isCoin = true;
        LogAnalyticsEvent("First_run_bonuce_001");
    }

    public void SetSpark()
    {
        isSpark = true;
        LogAnalyticsEvent("First_run_bonuce_001");
    }

    public void ResetAll()
    {
        isPlane = false;
        isBomb = false;
        isCoin = false;
        isSpark = false;
    }

    public void LogAnalyticsEvent(string eventName)
    {
        // Проверить, что экземпляр FirebaseManager существует
        if (firebaseManager != null)
        {
            // Вызвать метод логирования события из FirebaseManager
            firebaseManager.LogAnalyticsEvent(eventName);
        }
        else
        {
            Debug.LogError("FirebaseManager is not initialized.");
        }
    }
}


