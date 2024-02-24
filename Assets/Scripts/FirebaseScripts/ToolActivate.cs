using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class ToolActivate : MonoBehaviour
{
    [SerializeField] private Button activateToolButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        activateToolButton.onClick.AddListener(OnActivateToolButtonClick);
    }

    public void OnActivateToolButtonClick()
    {
        // ����� ��������� ����������� ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstRunToolActivate();
    }
}

