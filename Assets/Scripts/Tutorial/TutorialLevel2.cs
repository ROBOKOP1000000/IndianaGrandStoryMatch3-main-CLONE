using UnityEngine;
using UnityEngine.UI;

public class TutorialLevel2 : MonoBehaviour
{
    [SerializeField] private GameObject substrateObject; // объект подложки с вырезом
    [SerializeField] private GameObject tigraImage; // объект обучающей картинки
    [SerializeField] private GameObject handObject; // объект рука

    private void Awake()
    {
        // Поиск объектов по их именам
        substrateObject = GameObject.Find("Substrate");
        tigraImage = GameObject.Find("Tigra");
        handObject = GameObject.Find("hand");

    }
    public void SecondStep()
    {
        // Скрывает объекты при вызове этого метода
        substrateObject.SetActive(false);
        tigraImage.SetActive(false);
        handObject.SetActive(false);
    }

    public void ThirdStep()
    {
        // Отображает объекты при вызове этого метода
        substrateObject.SetActive(true);
        tigraImage.SetActive(true);
        handObject.SetActive(true);
    }
}
