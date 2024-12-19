using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    [SerializeField] public AudioSource[] musicSource; // Массив AudioSource для музыки

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        foreach (AudioSource source in musicSource)
        {
            source.volume = volume;
        }
    }
}