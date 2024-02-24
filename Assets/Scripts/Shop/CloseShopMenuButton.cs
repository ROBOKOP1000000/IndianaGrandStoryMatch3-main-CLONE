using UnityEngine;
using UnityEngine.UI;

public class CloseShopMenuButton : MonoBehaviour
{
    private Button closeButton;

    private void Start()
    {
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(CloseMenu);
    }

    private void CloseMenu()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
