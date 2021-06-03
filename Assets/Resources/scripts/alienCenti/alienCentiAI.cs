using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienCentiAI : MonoBehaviour
{
    private Rigidbody2D rig;
    private Vector2 dir;
    private Transform target;
    public float moveSpeed;
    public Transform segment;
    public float maxSpeed;

    public bool randSpawned = true;
    private Vector2 pos;

    private bool fastboi = false;
    private float segStep;

    void Start()
    {
        worldAI.obstacleEnemyList.Add(gameObject);
        rig = GetComponent<Rigidbody2D>();
        if(FindObjectOfType<pController>() != null)
        {
            target = FindObjectOfType<pController>().transform;
        }
        if (!randSpawned)
        {
            pos = transform.position;
        }
        if (worldAI.stage == 3)
        {
            fastboi = true;
            segStep = (target.transform.position - transform.position).magnitude * 3 * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            if (!fastboi)
            {
                if (segment != null)
                {
                    transform.up = (transform.position - segment.position);
                    rig.AddForce((target.transform.position - transform.position).normalized * moveSpeed);
                    if (rig.velocity.magnitude > maxSpeed)
                    {
                        rig.velocity = rig.velocity.normalized * maxSpeed;
                    }
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, segStep);
                transform.up = target.position - transform.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!fastboi)
        {
            if (collision2D.gameObject.layer == 12)
            {
                AudioSource.PlayClipAtPoint(Resources.Load("sounds/hit") as AudioClip, transform.position);
                worldAI.obstacleEnemyList.Remove(gameObject);
                /* Currently never in a part of the game that gives score
                if (!worldAI.gameOver)
                {
                    worldAI.score += 300;
                    worldAI.scoreUpdate = true;
                }
                */
                Destroy(gameObject);
            }
        }
    }
}
