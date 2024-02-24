using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferManager : MonoBehaviour
{
    public static bool isOffer5Day;
    public static bool isSpecialOffer;
    [SerializeField] private GameObject offer5Day;
    [SerializeField] private GameObject specialOffer;
    [SerializeField] private GameObject[] pos;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("isOffer5Day"))
        {
            PlayerPrefs.SetString("isOffer5Day", "false");
        }
        if (!PlayerPrefs.HasKey("isSpecialOffer"))
        {
            PlayerPrefs.SetString("isSpecialOffer", "false");
        }
        if (PlayerPrefs.GetString("isOffer5Day") == "true")
        {
            offer5Day.SetActive(true);
            for (int i = 0; i < pos.Length; i++)
            {
                if (!pos[i].GetComponent<Pos>().isFull)
                {
                    pos[i].GetComponent<Pos>().isFull = true;
                    offer5Day.transform.position = pos[i].transform.position;
                    break;
                }
            }
        }
        if (PlayerPrefs.GetString("isSpecialOffer") == "true")
        {
            specialOffer.SetActive(true);
            for (int i = 0; i < pos.Length; i++)
            {
                if (!pos[i].GetComponent<Pos>().isFull)
                {
                    pos[i].GetComponent<Pos>().isFull = true;
                    specialOffer.transform.position = pos[i].transform.position;
                    break;
                }
            }
        }
    }
}
