using UnityEngine;
using UnityEngine.UI;

public class OpenShopMenuButton : MonoBehaviour
{
    private Button openShopButton;
    [SerializeField] private GameObject shop;

    private void Start()
    {
        openShopButton = GetComponent<Button>();
        openShopButton.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {
        shop.gameObject.SetActive(true);
    }
}
