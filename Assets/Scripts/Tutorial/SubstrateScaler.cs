using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SubstrateScaler : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasScaler canvasScaler;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasScaler = canvas.GetComponent<CanvasScaler>();

        if (rectTransform != null && canvas != null && canvasScaler != null)
        {
            // Установка размеров RectTransform в размеры канваса
            rectTransform.sizeDelta = new Vector2(canvasScaler.referenceResolution.x, canvasScaler.referenceResolution.y);
            rectTransform.anchoredPosition = Vector2.zero; // Центрируем объект в канвасе
        }
    }
}
