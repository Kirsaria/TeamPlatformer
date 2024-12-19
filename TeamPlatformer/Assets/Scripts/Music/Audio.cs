using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioSource musicSource;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSource.volume = savedVolume;
            musicVolumeSlider.value = savedVolume;
        }
        else
        {
            musicSource.volume = 0.5f;
            musicVolumeSlider.value = 0.5f;
        }

        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}