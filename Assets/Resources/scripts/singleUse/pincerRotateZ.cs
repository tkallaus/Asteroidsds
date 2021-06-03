using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pincerRotateZ : MonoBehaviour
{
    private bool pincerForward = false;
    void FixedUpdate()
    {
        if (pincerForward)
        {
            transform.Rotate(0, 0, 3);
            if (transform.localRotation.eulerAngles.z > 60)
            {
                pincerForward = false;
            }
        }
        else
        {
            transform.Rotate(0, 0, -3);
            if (transform.localRotation.eulerAngles.z < 20)
            {
                pincerForward = true;
            }
        }
    }
}
