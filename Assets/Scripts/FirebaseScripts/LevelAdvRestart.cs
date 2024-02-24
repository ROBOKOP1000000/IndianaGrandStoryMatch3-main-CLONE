using Analytics;
using UnityEngine;
using UnityEngine.UI;

public class LevelAdvRestart : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    public void OnRestartButtonClick()
    {
        // После перезапуска уровня регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstRunLevelAdvRestart();
    }
}

