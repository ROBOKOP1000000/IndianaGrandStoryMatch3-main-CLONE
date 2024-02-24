using Analytics;
using UnityEngine;
using UnityEngine.UI;

public class OpenAdvertising : MonoBehaviour
{
    [SerializeField] private Button adButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        adButton.onClick.AddListener(OnAdButtonClick);
    }

    public void OnAdButtonClick()
    {


        // ����� ��������� ������� ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstClickAdvertising();
    }
}
