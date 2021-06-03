using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booletHit : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3);
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
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
}
