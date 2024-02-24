using Analytics;
using UnityEngine;
using UnityEngine.UI;

public class LevelAdvRestart : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    public void OnRestartButtonClick()
    {
        // ����� ����������� ������ ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstRunLevelAdvRestart();
    }
}

