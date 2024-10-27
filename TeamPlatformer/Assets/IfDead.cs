using UnityEngine;

public class IfDead : MonoBehaviour
{
    public GameObject NPSBeforeChase;
    public GameObject NPSInChase;
    public PlayerController playerController;
    public Dialog dialog;


    void Update()
    {
        if (playerController.isDead == true)
        {
            NPSInChase.SetActive(false);
            NPSBeforeChase.SetActive(true);
            dialog.isDialogEnd = false;
        }
    }
}