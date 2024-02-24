
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private AudioSource buttonMusic;

    public void Click()
    {
        if (AudioManager.instance.isSound)
        {
            buttonMusic.Play();

        }
    }
}
