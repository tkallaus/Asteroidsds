using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float shootTimer = 3f;
    public float moveSpeed = 1f;
    private int rotateDirection;
    public float maxSpeed;

    private Rigidbody2D rig;
    public Rigidbody2D boolet;
    public Transform booletSpawn;
    public float booletSpeed = 1f;
    private Transform target;
    private GameObject targetOrbit;

    static public GameObject[] possibleTargets;

    public bool randSpawned = true;
    private Vector2 pos;

    private Vector3 check;
    private void Start()
    {
        target = FindObjectOfType<pController>().transform;
        check = Random.insideUnitCircle.normalized * 4;
        if(check == Vector3.zero)
        {
            check = Vector2.up * 4;
        }
        targetOrbit = new GameObject();
        targetOrbit.transform.position = target.transform.position + check;
        targetOrbit.transform.SetParent(target);
        rotateDirection = Random.Range(0, 2) * 2 - 1;
        rig = GetComponent<Rigidbody2D>();

        worldAI.obstacleEnemyList.Add(gameObject);
        if (!randSpawned)
        {
            pos = transform.position;
        }
    }
    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Rigidbody2D cloneB = Instantiate(boolet, booletSpawn.position, Quaternion.identity) as Rigidbody2D;
            cloneB.AddForce(booletSpawn.up * booletSpeed);
            shootTimer = 3f;
        }
        if (worldAI.screenWrapActive)
        {
            ScreenWrap();
            if(possibleTargets == null)
            {
                possibleTargets = GameObject.FindGameObjectsWithTag("pWrap");
            }
            for (int i = 0; i < possibleTargets.Length; i++)
            {
                if ((possibleTargets[i].transform.position - transform.position).sqrMagnitude < (target.position - transform.position).sqrMagnitude)
                {
                    target = possibleTargets[i].transform;
                    targetOrbit.transform.position = target.transform.position + check;
                    targetOrbit.transform.SetParent(target);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        transform.up = target.position - transform.position;
        targetOrbit.transform.RotateAround(target.position, Vector3.forward, rotateDirection * .5f);
        rig.AddForce((targetOrbit.transform.position - transform.position).normalized * moveSpeed);
        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == 12)
        {
            AudioSource.PlayClipAtPoint(Resources.Load("sounds/hit") as AudioClip, transform.position);
            worldAI.obstacleEnemyList.Remove(gameObject);

            if (!worldAI.gameOver)
            {
                worldAI.score += 300;
                worldAI.scoreUpdate = true;
            }

            worldAI.particleSpawn(transform.position);

            Destroy(targetOrbit);
            Destroy(gameObject);
        }
    }
    void ScreenWrap()
    {
        Vector3 newPos = transform.position;

        if (newPos.x > worldAI.HalfScreenWidth + (worldAI.HalfScreenWidth * 2) * worldAI.camGrid.x)
        {
            newPos.x = -worldAI.HalfScreenWidth + (worldAI.HalfScreenWidth * 2) * worldAI.camGrid.x;
        }
        if (newPos.x < -worldAI.HalfScreenWidth + (worldAI.HalfScreenWidth * 2) * worldAI.camGrid.x)
        {
            newPos.x = worldAI.HalfScreenWidth + (worldAI.HalfScreenWidth * 2) * worldAI.camGrid.x;
        }
        if (newPos.y > worldAI.HalfScreenHeight + (worldAI.HalfScreenHeight * 2) * worldAI.camGrid.y)
        {
            newPos.y = -worldAI.HalfScreenHeight + (worldAI.HalfScreenHeight * 2) * worldAI.camGrid.y;
        }
        if (newPos.y < -worldAI.HalfScreenHeight + (worldAI.HalfScreenHeight * 2) * worldAI.camGrid.y)
        {
            newPos.y = worldAI.HalfScreenHeight + (worldAI.HalfScreenHeight * 2) * worldAI.camGrid.y;
        }

        transform.position = newPos;
    }
}
