using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randDeactivate : MonoBehaviour
{
    private float deactTimer;

    private void Start()
    {
        deactTimer = Random.Range(0f, 0.3f);
    }
    void Update()
    {
        deactTimer -= Time.deltaTime;
        if(deactTimer < 0f)
        {
            deactTimer = Random.Range(0f, 0.3f);
            gameObject.SetActive(false);
        }
    }
}
