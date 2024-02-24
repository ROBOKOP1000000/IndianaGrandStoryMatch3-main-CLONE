using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeAudioButton : MonoBehaviour
{
    [SerializeField] private Sprite onSound;
    [SerializeField] private Sprite offSound;
    [SerializeField] private Sprite backgroundOnSound;
    [SerializeField] private Sprite backgroundOffSound;

    [SerializeField] private Sprite onMusic;
    [SerializeField] private Sprite offMusic;
    [SerializeField] private Sprite backgroundOnMusic;
    [SerializeField] private Sprite backgroundOffMusic;


    [SerializeField] private Button sound;
    [SerializeField] private Button music;
    [SerializeField] private Image soundIcon;
    [SerializeField] private Image musicIcon;
    [SerializeField] private Sound backgroundSound;

    private void Start()
    {
        if (AudioManager.instance)
        {

            if (AudioManager.instance.isSound)
            {
                sound.image.sprite = backgroundOnSound;
                soundIcon.sprite = onSound;
            }
            else
            {
                sound.image.sprite = backgroundOffSound;
                soundIcon.sprite = offSound;
            }
            if (AudioManager.instance.isMusic)
            {
                music.image.sprite = backgroundOnMusic;
                musicIcon.sprite = onMusic;
                backgroundSound.PlaySound();
            }
            else
            {
                music.image.sprite = backgroundOffMusic;
                musicIcon.sprite = offMusic;
                backgroundSound.StopSound();
            }
        }
    }
    public void SetSound()
    {
        AudioManager.instance.isSound = !AudioManager.instance.isSound;
        if (AudioManager.instance.isSound)
        {
            sound.image.sprite = backgroundOnSound;
            soundIcon.sprite = onSound;
        }
        else
        {
            sound.image.sprite = backgroundOffSound;
            soundIcon.sprite = offSound;
        }
    }
    public void SetMusic()
    {
        AudioManager.instance.isMusic = !AudioManager.instance.isMusic;
        if (AudioManager.instance.isMusic)
        {
            music.image.sprite = backgroundOnMusic;
            musicIcon.sprite = onMusic;
            backgroundSound.PlaySound();
        }
        else
        {
            music.image.sprite = backgroundOffMusic;
            musicIcon.sprite = offMusic;
            backgroundSound.StopSound();
        }
    }
}
