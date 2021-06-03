using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFight : MonoBehaviour
{
    public GameObject barrier1;
    private bool theOneTime = true;
    static public bool bossGo = false;
    public tentacleHP tent1;
    public tentacleHP tent2;
    public tentacleHP tent3;
    public GameObject bigReveal;
    void Update()
    {
        if (worldAI.gameOver && !theOneTime)
        {
            theOneTime = true;
            worldAI.screenWrapActive = false;
            barrier1.SetActive(false);
            bossGo = false;
            tent1.HP = 30;
            tent2.HP = 30;
            tent3.HP = 30;
            tent1.dead = false;
            tent2.dead = false;
            tent3.dead = false;
            bigReveal.SetActive(true);
        }
        if (tent1.dead && tent2.dead && tent3.dead)
        {
            bigReveal.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (theOneTime && collision.gameObject.layer == 8)
        {
            worldAI.bossTime = true;
            worldAI.screenWrapActive = true;
            barrier1.SetActive(true);
            theOneTime = false;
        }
    }
}
