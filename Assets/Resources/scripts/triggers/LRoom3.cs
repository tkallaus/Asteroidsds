using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRoom3 : MonoBehaviour
{
    public GameObject closeDoor;
    public GameObject barrier;
    public GameObject barrier2;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    private GameObject enemy1;
    private GameObject enemy2;
    private Object enemy3;
    private Object enemy4;
    private bool theOneTime = true;
    void Update()
    {
        if (worldAI.gameOver && !theOneTime)
        {
            theOneTime = true;
            Destroy(enemy1, 1);
            Destroy(enemy2, 1);
            Destroy(enemy3, 1);
            Destroy(enemy4, 1);
            closeDoor.SetActive(false);
        }
        if (enemy1 == null && enemy2 == null && enemy3 == null && enemy4 == null)
        {
            barrier.SetActive(false);
            barrier2.SetActive(false);
        }
        else
        {
            barrier.SetActive(true);
            barrier2.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if(enemy1 != null)
        {
            enemy1.transform.position = Vector3.MoveTowards(enemy1.transform.position, spawn1.position, 2);
        }
        if (enemy2 != null)
        {
            enemy2.transform.position = Vector3.MoveTowards(enemy2.transform.position, spawn2.position, 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theOneTime && collision.gameObject.layer == 8)
        {
            closeDoor.SetActive(true);
            worldAI.screenWrapActive = true;
            enemy1 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn1.position, Quaternion.identity) as GameObject;
            enemy2 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn2.position, Quaternion.identity) as GameObject;
            enemy3 = Instantiate(Resources.Load("prefabs/enemies/centiContainer"), spawn3.position, Quaternion.identity);
            enemy4 = Instantiate(Resources.Load("prefabs/enemies/centiContainer"), spawn4.position, Quaternion.identity);
            theOneTime = false;
        }
    }
}
