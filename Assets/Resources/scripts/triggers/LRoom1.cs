using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRoom1 : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    private Object centi1;
    private Object centi2;
    public GameObject barrier1;
    public GameObject barrier2;
    public GameObject closeDoor;
    public GameObject closeDoor2;
    public GameObject blocker1;
    private bool theOneTime = true;

    void Update()
    {
        if (worldAI.gameOver && !theOneTime)
        {
            theOneTime = true;
            Destroy(centi1, 1);
            Destroy(centi2, 1);
            closeDoor.SetActive(false);
            closeDoor2.SetActive(false);
        }
        if (centi1 == null && centi2 == null)
        {
            barrier1.SetActive(false);
            barrier2.SetActive(false);
            closeDoor2.SetActive(false);
            blocker1.SetActive(false);
        }
        else
        {
            barrier1.SetActive(true);
            barrier2.SetActive(true);
            closeDoor2.SetActive(true);
            blocker1.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theOneTime && collision.gameObject.layer == 8)
        {
            centi1 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn1.position, Quaternion.identity);
            centi2 = Instantiate(Resources.Load("prefabs/enemies/enemyShip"), spawn2.position, Quaternion.identity);
            closeDoor.SetActive(true);
            theOneTime = false;
        }
    }
}
