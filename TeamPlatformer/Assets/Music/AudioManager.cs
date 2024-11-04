using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

}