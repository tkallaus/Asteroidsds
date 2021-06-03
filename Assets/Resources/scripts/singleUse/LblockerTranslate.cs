using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LblockerTranslate : MonoBehaviour
{
    private bool swit = false;
    public Transform spot1;
    public Transform spot2;
    private void FixedUpdate()
    {
        if (transform.position.y > -90)
        {
            swit = false;
        }
        if (transform.position.y < -110)
        {
            swit = true;
        }
        if (swit)
        {
            transform.position = Vector3.MoveTowards(transform.position, spot1.position, 10 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spot2.position, 10 * Time.deltaTime);
        }
    }
}
