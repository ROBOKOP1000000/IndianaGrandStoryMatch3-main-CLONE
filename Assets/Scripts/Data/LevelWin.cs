using UnityEngine;
using Analytics;


public class LevelWin : MonoBehaviour
{
    private FirebaseManager firebaseManager;

    void Start()
    {
        firebaseManager = FirebaseManager.Instance;

        if (firebaseManager == null)
        {
            Debug.LogError("FirebaseManager not found.");
        }
    }

    public void LogAnalyticsEvent(string eventName)
    {
        if (firebaseManager != null)
        {
            firebaseManager.LogAnalyticsEvent(eventName);
        }
        else
        {
            Debug.LogError("FirebaseManager is not initialized.");
        }
    }

    public void WinLevel001()
    {
        LogAnalyticsEvent("level_001_win");
        Debug.Log("Level 1 won");
    }

    public void WinLevel002()
    {
        LogAnalyticsEvent("level_002_win");
        Debug.Log("Level 2 won");
    }

    public void WinLevel003()
    {
        LogAnalyticsEvent("level_003_win");
        Debug.Log("Level 3 won");
    }

    public void WinLevel004()
    {
        LogAnalyticsEvent("level_004_win");
        Debug.Log("Level 4 won");
    }

    public void WinLevel005()
    {
        LogAnalyticsEvent("level_005_win");
        Debug.Log("Level 5 won");
    }

    public void WinLevel006()
    {
        LogAnalyticsEvent("level_006_win");
        Debug.Log("Level 6 won");
    }

    public void WinLevel007()
    {
        LogAnalyticsEvent("level_007_win");
        Debug.Log("Level 7 won");
    }

    public void WinLevel008()
    {
        LogAnalyticsEvent("level_008_win");
        Debug.Log("Level 8 won");
    }

    public void WinLevel009()
    {
        LogAnalyticsEvent("level_009_win");
        Debug.Log("Level 9 won");
    }

    public void WinLevel010()
    {
        LogAnalyticsEvent("level_010_win");
        Debug.Log("Level 10 won");
    }

    public void WinLevel011()
    {
        LogAnalyticsEvent("level_011_win");
        Debug.Log("Level 11 won");
    }

    public void WinLevel012()
    {
        LogAnalyticsEvent("level_012_win");
        Debug.Log("Level 12 won");
    }

    public void WinLevel013()
    {
        LogAnalyticsEvent("level_013_win");
        Debug.Log("Level 13 won");
    }

    public void WinLevel014()
    {
        LogAnalyticsEvent("level_014_win");
        Debug.Log("Level 14 won");
    }

    public void WinLevel015()
    {
        LogAnalyticsEvent("level_015_win");
        Debug.Log("Level 15 won");
    }

    public void WinLevel016()
    {
        LogAnalyticsEvent("level_016_win");
        Debug.Log("Level 16 won");
    }
}