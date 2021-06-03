using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleRotateY : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 25, 0);
    }
}
