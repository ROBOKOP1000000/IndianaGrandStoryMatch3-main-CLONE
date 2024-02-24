using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public bool isSound;
    public bool isMusic;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            isSound = true;
            isMusic = true;
            DontDestroyOnLoad(this);
        }
    }
}
