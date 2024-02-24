using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class BuyLifeForAdvertising : MonoBehaviour
{
    [SerializeField] private Button buyLifeForAdButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        buyLifeForAdButton.onClick.AddListener(OnBuyLifeForAdButtonClick);
    }

    public void OnBuyLifeForAdButtonClick()
    {
        // �������� ��� ��� ��� ������� ����� �� �������� ������� �����
        // ...

        // ����� ������� ����� ����� �������� ������� ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstRunAdvBuyLife();
    }
}
