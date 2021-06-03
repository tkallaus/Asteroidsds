using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidAI : MonoBehaviour
{
    private Rigidbody2D rig;

    public int asteroidSize;
    public bool randSpawned = true;
    private Vector2 pos;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 0, Random.Range(0, 360));
        rig.angularVelocity = Random.Range(10, 20) * (Random.Range(0,2)*2-1);

        if (!randSpawned && worldAI.bossTime)
        {
            rig.velocity = new Vector2(Random.Range(-0.9f, 0.9f), Random.Range(-1f, -0.2f)) * Random.Range(1f,5f);
        }
        else if (asteroidSize == 3)
        {
            rig.velocity = new Vector2((Random.Range(0, 2) * 2 - 1), (Random.Range(0, 2) * 2 - 1)) * 2;
        }
        else
        {
            rig.velocity = Random.insideUnitCircle.normalized * Random.Range(1, 5);
            if (rig.velocity == Vector2.zero)
            {
                rig.velocity = new Vector2(2, 3);
            }
        }

        worldAI.obstacleEnemyList.Add(gameObject);
        if (!randSpawned)
        {
            pos = transform.position;
        }
    }
    private void Update()
    {
        if (worldAI.screenWrapActive)
        {
            ScreenWrap();
        }
    }
    void ScreenWrap()
    {
        Vector3 newPos = transform.position;
        if (!worldAI.bossTime)
        {
            if (newPos.x > worldAI.HalfScreenWidth + asteroidSize)
            {
                newPos.x = -worldAI.HalfScreenWidth - asteroidSize;
            }
            if (newPos.x < -worldAI.HalfScreenWidth - asteroidSize)
            {
                newPos.x = worldAI.HalfScreenWidth + asteroidSize;
            }
            if (newPos.y > worldAI.HalfScreenHeight + asteroidSize)
            {
                newPos.y = -worldAI.HalfScreenHeight - asteroidSize;
            }
            if (newPos.y < -worldAI.HalfScreenHeight - asteroidSize)
            {
                newPos.y = worldAI.HalfScreenHeight + asteroidSize;
            }
        }
        else
        {
            if (newPos.x > worldAI.HalfScreenWidth)
            {
                newPos.x = -worldAI.HalfScreenWidth;
            }
            if (newPos.x < -worldAI.HalfScreenWidth)
            {
                newPos.x = worldAI.HalfScreenWidth;
            }
        }
        transform.position = newPos;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.layer == 12 || collision2D.gameObject.layer == 15)
        {
            if(asteroidSize == 3) //Special alien astroid
            {
                Instantiate(Resources.Load("prefabs/enemies/centiContainer"), transform.position, Quaternion.identity);
            }
            else if(asteroidSize == 2)
            {
                Instantiate(Resources.Load("prefabs/obstacles/asteroidM" + Random.Range(0, 3)), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("prefabs/obstacles/asteroidM" + Random.Range(0, 3)), transform.position, Quaternion.identity);
            }
            else if(asteroidSize == 1)
            {
                Instantiate(Resources.Load("prefabs/obstacles/asteroidS" + Random.Range(0, 2)), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("prefabs/obstacles/asteroidS" + Random.Range(0, 2)), transform.position, Quaternion.identity);
                Instantiate(Resources.Load("prefabs/obstacles/asteroidS" + Random.Range(0, 2)), transform.position, Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(Resources.Load("sounds/hit") as AudioClip, transform.position, 0.5f);
            worldAI.obstacleEnemyList.Remove(gameObject);

            if (!worldAI.gameOver)
            {
                worldAI.score += 50 * (asteroidSize+1);
                worldAI.scoreUpdate = true;
            }

            worldAI.particleSpawn(transform.position);

            Destroy(gameObject);
        }
    }
}
