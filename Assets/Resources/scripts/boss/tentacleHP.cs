using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleHP : MonoBehaviour
{
    public int HP;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public bool dead = false;
    private MeshRenderer r1;
    private MeshRenderer r2;
    private MeshRenderer r3;
    private PolygonCollider2D c1;
    private PolygonCollider2D c2;
    private PolygonCollider2D c3;
    private void Start()
    {
        r1 = p1.GetComponent<MeshRenderer>();
        r2 = p2.GetComponent<MeshRenderer>();
        r3 = p3.GetComponent<MeshRenderer>();
        c1 = p1.GetComponent<PolygonCollider2D>();
        c2 = p2.GetComponent<PolygonCollider2D>();
        c3 = p3.GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if(HP <= 20)
        {
            r3.enabled = false;
            c3.enabled = false;
            if (HP <= 10)
            {
                r2.enabled = false;
                c2.enabled = false;
                if (HP <= 0)
                {
                    r1.enabled = false;
                    c1.enabled = false;
                    dead = true;
                }
            }
        }
        else
        {
            r1.enabled = true;
            c1.enabled = true;
            r2.enabled = true;
            c2.enabled = true;
            r3.enabled = true;
            c3.enabled = true;
            dead = false;
        }
    }
}
