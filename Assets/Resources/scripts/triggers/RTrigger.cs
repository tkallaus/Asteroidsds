using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTrigger : MonoBehaviour
{
    private bool theOneTime = true;
    public LSwitch pipe1;
    public GameObject barrier1;
    public LSwitch pipe2;
    public GameObject barrier2;

    void Update()
    {
        if (worldAI.gameOver && !theOneTime)
        {
            theOneTime = true;
        }
        if (pipe1.active)
        {
            barrier1.SetActive(false);
        }
        if (pipe2.active)
        {
            barrier2.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theOneTime && collision.gameObject.layer == 8)
        {
            barrier1.SetActive(true);
            barrier2.SetActive(true);
            theOneTime = false;
        }
    }
}
