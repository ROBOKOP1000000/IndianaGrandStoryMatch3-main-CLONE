using UnityEngine;
using Analytics;
using UnityEngine.UI;

public class FirstRunLevelRun : MonoBehaviour
{
    [SerializeField] private Button playButton;

    void Start()
    {
        // ��������� ���������� ������� �� ������
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    public void OnPlayButtonClick()
    {
        // ����� ������� ������ Play ������������ ������� � Firebase Analytics
        FirebaseManager.Instance.FirstLevelOpen();
    }
}


