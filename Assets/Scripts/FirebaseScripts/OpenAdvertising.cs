using Analytics;
using UnityEngine;
using UnityEngine.UI;

public class OpenAdvertising : MonoBehaviour
{
    [SerializeField] private Button adButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        adButton.onClick.AddListener(OnAdButtonClick);
    }

    public void OnAdButtonClick()
    {


        // После просмотра рекламы регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstClickAdvertising();
    }
}
