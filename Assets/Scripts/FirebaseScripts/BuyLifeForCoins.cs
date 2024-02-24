using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class BuyLifeForCoins : MonoBehaviour
{
    [SerializeField] private Button buyLifeButton;

    void Start()
    {
        // Добавляем обработчик нажатия на кнопку
        buyLifeButton.onClick.AddListener(OnBuyLifeButtonClick);
    }

    public void OnBuyLifeButtonClick()
    {
        // Помещаем ваш код для покупки жизни за монеты здесь
        // ...

        // После покупки жизни регистрируем событие в Firebase Analytics
        FirebaseManager.Instance.FirstRunMoneyBuyLife();
    }
}
