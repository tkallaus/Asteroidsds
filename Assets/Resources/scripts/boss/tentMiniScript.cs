using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentMiniScript : MonoBehaviour
{
    private tentacleHP HPizzle;
    private float colorTimer;
    private bool gotHit;
    private Outline color;
    private void Start()
    {
        HPizzle = gameObject.GetComponentInParent<tentacleHP>();
        colorTimer = 0.5f;
        gotHit = false;
        color = GetComponent<Outline>();
    }
    private void FixedUpdate()
    {
        if (gotHit)
        {
            color.OutlineColor = Color.white;
            colorTimer -= Time.deltaTime;
            if(colorTimer <= 0f)
            {
                color.OutlineColor = Color.green;
                colorTimer = 0.5f;
                gotHit = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 12) && !worldAI.gameOver && worldAI.bossTime)
        {
            HPizzle.HP -= 1;
            gotHit = true;
            bossFight.bossGo = true;
        }
    }
}
