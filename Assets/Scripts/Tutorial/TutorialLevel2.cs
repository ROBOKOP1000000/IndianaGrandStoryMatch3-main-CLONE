using UnityEngine;
using UnityEngine.UI;

public class TutorialLevel2 : MonoBehaviour
{
    [SerializeField] private GameObject substrateObject; // ������ �������� � �������
    [SerializeField] private GameObject tigraImage; // ������ ��������� ��������
    [SerializeField] private GameObject handObject; // ������ ����

    private void Awake()
    {
        // ����� �������� �� �� ������
        substrateObject = GameObject.Find("Substrate");
        tigraImage = GameObject.Find("Tigra");
        handObject = GameObject.Find("hand");

    }
    public void SecondStep()
    {
        // �������� ������� ��� ������ ����� ������
        substrateObject.SetActive(false);
        tigraImage.SetActive(false);
        handObject.SetActive(false);
    }

    public void ThirdStep()
    {
        // ���������� ������� ��� ������ ����� ������
        substrateObject.SetActive(true);
        tigraImage.SetActive(true);
        handObject.SetActive(true);
    }
}
