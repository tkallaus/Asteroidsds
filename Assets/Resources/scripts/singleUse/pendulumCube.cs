using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulumCube : MonoBehaviour
{
    public float maxDistance;
    public float speed;
    private float xOffset;
    private void Awake()
    {
        xOffset = transform.position.x;
    }
    private void FixedUpdate()
    {
        float pos = maxDistance * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(pos+xOffset, transform.position.y);
        //speed += 0.01f; //fun
    }
}
