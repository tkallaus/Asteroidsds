using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRoom2 : MonoBehaviour
{
    private bool theOneTime = true;
    public GameObject wrapEffect1;
    public GameObject wrapEffect2;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    public LSwitch pipe1;
    public LSwitch pipe2;
    public LSwitch pipe3;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    private bool justInCase = true;

    void Update()
    {
        if (worldAI.gameOver && !theOneTime)
        {
            wrapEffect1.SetActive(false);
            wrapEffect2.SetActive(false);
            Destroy(enemy1, 1);
            Destroy(enemy2, 1);
            Destroy(enemy3, 1);
            worldAI.screenWrapActive = false;
            theOneTime = true;
            justInCase = true;
        }
        if (pipe1.active && pipe2.active && pipe3.active)
        {
            wrapEffect1.SetActive(false);
            wrapEffect2.SetActive(false);
            if (justInCase)
            {
                worldAI.screenWrapActive = false;
                justInCase = false;
            }
            if(enemy1 != null)
            {
                Destroy(enemy1);
            }
            if (enemy2 != null)
            {
                Destroy(enemy2);
            }
            if (enemy3 != null)
            {
                Destroy(enemy3);
            }
        }
    }
    private void FixedUpdate()
    {
        if (enemy1 != null)
        {
            enemy1.transform.position = Vector3.MoveTowards(enemy1.transform.position, spawn1.position, 2);
        }
        if (enemy2 != null)
        {
            enemy2.transform.position = Vector3.MoveTowards(enemy2.transform.position, spawn2.position, 2);
        }
        if (enemy3 != null)
        {
            enemy3.transform.position = Vector3.MoveTowards(enemy3.transform.position, spawn3.position, 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theOneTime && collision.gameObject.layer == 8)
        {
            wrapEffect1.SetActive(true);
            wrapEffect2.SetActive(true);
            worldAI.screenWrapActive = true;
            enemy1 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn1.position, Quaternion.identity) as GameObject;
            enemy2 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn2.position, Quaternion.identity) as GameObject;
            enemy3 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn3.position, Quaternion.identity) as GameObject;
            theOneTime = false;
        }
    }
}
