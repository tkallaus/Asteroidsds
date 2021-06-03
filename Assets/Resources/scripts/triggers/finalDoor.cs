using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoor : MonoBehaviour
{
    static public int doorLock;
    private void Start()
    {
        doorLock = 0;
    }
    void Update()
    {
        if(doorLock == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
