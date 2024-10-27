using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Dialog dialog;
    public GameObject NPSBeforeChase;
    public GameObject NPSInChase;
    public GameObject Barrier;
    public PlayerController playerController;

    void Update()
    {
        if (dialog.isDialogEnd)
        {
            Barrier.SetActive(false);
            StartCoroutine(StartChaseAfterDelay(2f));
        }
    }

    IEnumerator StartChaseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NPSInChase.SetActive(true);
        NPSBeforeChase.SetActive(false);
    }
}