using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSwitch : MonoBehaviour
{
    public bool active;
    private float timer = 0f;
    public float timerMax = 2f;
    private void Start()
    {
        active = false;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f && active)
        {
            active = false;
            GetComponent<Outline>().OutlineColor = Color.red;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        active = true;
        timer = timerMax;
        GetComponent<Outline>().OutlineColor = Color.green;
    }
}
