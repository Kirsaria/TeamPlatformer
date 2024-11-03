using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenu()
    {
        StopMusic();
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("SceneMainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void StopMusic()
    {
        GameObject musicObject = GameObject.FindWithTag("music");
        if (musicObject != null)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                Destroy(musicObject);
            }
        }
    }
}