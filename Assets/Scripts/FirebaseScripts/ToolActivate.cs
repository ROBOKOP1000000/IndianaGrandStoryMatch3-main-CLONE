using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class ToolActivate : MonoBehaviour
{
    [SerializeField] private Button activateToolButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        activateToolButton.onClick.AddListener(OnActivateToolButtonClick);
    }

    public void OnActivateToolButtonClick()
    {
        // После активации инструмента регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstRunToolActivate();
    }
}

