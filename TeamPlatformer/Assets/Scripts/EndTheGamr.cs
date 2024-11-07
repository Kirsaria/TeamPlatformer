using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTheGame : MonoBehaviour
{
    public Dialog dialog;
    public Dialog dialogWithGirl;
    public PlayerControllerFinal playerController;
    public GameObject targetGameObject;

    private bool dialogStarted = false;

    void Update()
    {
        if (dialogWithGirl.isDialogEnd && !dialogStarted)
        {
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(false);
                StartCoroutine(StartDialog1());
            }

        }

        if (dialog.isDialogEnd && dialogStarted)
        {
            StartCoroutine(GoToMainMenu());
        }
    }

    private IEnumerator StartDialog1()
    {
        yield return new WaitForSeconds(1f);
        playerController.speed = 0;
        playerController.jumpForce = 0;
        dialog.StartDialog();
        dialogStarted = true;
        dialog.isPlayerInRange = true;

    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("SceneMainMenu");
    }
}