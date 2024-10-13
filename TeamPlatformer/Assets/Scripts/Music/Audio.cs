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
        musicVolumeSlider.value = musicSource.volume;
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

}
