using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class BuyLifeForAdvertising : MonoBehaviour
{
    [SerializeField] private Button buyLifeForAdButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        buyLifeForAdButton.onClick.AddListener(OnBuyLifeForAdButtonClick);
    }

    public void OnBuyLifeForAdButtonClick()
    {
        // Помещаем ваш код для покупки жизни за просмотр рекламы здесь
        // ...

        // После покупки жизни через просмотр рекламы регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstRunAdvBuyLife();
    }
}
