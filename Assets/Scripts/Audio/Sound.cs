using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource sourse;
    private void Start()
    {
        sourse = GetComponent<AudioSource>();
        if (AudioManager.instance != null)
        {
            if (AudioManager.instance.isMusic)
            {
                PlaySound();
            }
            else
            {
                StopSound();
            }
        }
    }

    public void PlaySound()
    {
        sourse.Play();
    }
    public void StopSound()
    {
        sourse.Stop();
    }
}
