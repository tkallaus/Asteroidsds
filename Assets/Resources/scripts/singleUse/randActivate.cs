using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randActivate : MonoBehaviour
{
    static public bool staticGo = false;
    
    void Update()
    {
        if (staticGo)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            GetComponent<AudioSource>().Play();
            staticGo = false;
        }
    }
}
