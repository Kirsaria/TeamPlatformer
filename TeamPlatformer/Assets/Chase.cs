using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Dialog dialog;
    public GameObject NPSBeforeChase;
    public GameObject NPSInChase;
    public PlayerController playerController;

    void Update()
    {
        if (dialog.isDialogEnd)
        {
            NPSInChase.SetActive(true);
            NPSBeforeChase.SetActive(false);
        }

    }
}
