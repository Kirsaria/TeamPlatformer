using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScenePortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
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
            SceneManager.LoadScene("FinalScene");
        }
    }
}