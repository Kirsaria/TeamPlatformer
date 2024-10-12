using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objc = GameObject.FindGameObjectsWithTag("music");

        if (objc.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
