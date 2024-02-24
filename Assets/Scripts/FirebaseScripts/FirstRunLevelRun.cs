using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class FirstRunLevelRun : MonoBehaviour
{
    [SerializeField] private Button playButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    public void OnPlayButtonClick()
    {
        // После нажатия кнопки Play регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstLevelOpen();
    }
}


