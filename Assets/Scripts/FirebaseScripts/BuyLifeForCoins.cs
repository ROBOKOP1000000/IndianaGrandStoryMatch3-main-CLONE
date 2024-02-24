using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class BuyLifeForCoins : MonoBehaviour
{
    [SerializeField] private Button buyLifeButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        buyLifeButton.onClick.AddListener(OnBuyLifeButtonClick);
    }

    public void OnBuyLifeButtonClick()
    {
        // �������� ��� ��� ��� ������� ����� �� ������ �����
        // ...

        // ����� ������� ����� ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstRunMoneyBuyLife();
    }
}
