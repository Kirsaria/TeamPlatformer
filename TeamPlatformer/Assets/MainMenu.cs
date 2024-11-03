using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject musicObject = GameObject.FindWithTag("musicMaint");
        if (musicObject != null)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        FindObjectOfType<AudioManager>().PlayMusic();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
