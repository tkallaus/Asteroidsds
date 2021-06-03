using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segmentAI : MonoBehaviour
{
    private float deathTimer = 1f;
    public Transform leader;
    private float segmentStep = 1f;
    public bool leaderDead = false;
    private segmentAI seg;
    private bool fastdie = false;

    private void Start()
    {
        seg = leader.GetComponent<segmentAI>();
        if (worldAI.stage == 3)
        {
            fastdie = true;
        }
    }
    void Update()
    {
        if (leader == null || leaderDead)
        {
            leaderDead = true;
            if (deathTimer < 0)
            {
                Destroy(gameObject);
            }
            else if(deathTimer == 1f)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
            }
            deathTimer -= Time.deltaTime;

            if (fastdie)
            {
                Destroy(gameObject);
            }
        }
        else if (seg != null)
        {
            if (seg.leaderDead)
            {
                leaderDead = true;
            }
        }
        if(!leaderDead)
        {
            segmentStep = (leader.position - transform.position).magnitude * 12 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, leader.transform.position, segmentStep);
            transform.up = leader.position - transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 12)
        {
            Destroy(gameObject);
        }
        
    }
}
