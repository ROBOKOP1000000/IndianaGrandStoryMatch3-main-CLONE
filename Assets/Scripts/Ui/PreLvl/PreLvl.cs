using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLvl : MonoBehaviour
{
    [SerializeField] private GameObject[] preLvls;

    private void Start()
    {
        preLvls[PlayerPrefs.GetInt("CurrentLvl")-1].SetActive(true);
    }
}
