﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingCubesL: MonoBehaviour
{
    private bool go = true;
    private float timer = 2f;
    private void FixedUpdate()
    {
        if (go)
        {
            transform.Translate(0, 3 * Time.deltaTime, 0);
        }
        if (transform.position.y > 7)
        {
            go = false;
            transform.position = new Vector3(transform.position.x, 0);
        }
        if (!go)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            go = true;
            timer = 2f;
        }
    }
}
