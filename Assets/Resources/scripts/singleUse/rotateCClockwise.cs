using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCClockwise : MonoBehaviour
{
    public float debug;
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, debug);
    }
}
