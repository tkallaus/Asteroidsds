using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject obsEnemy in worldAI.obstacleEnemyList)
        {
            Destroy(obsEnemy);
        }
        worldAI.obstacleEnemyList.Clear();
        worldAI.obstacleEnemyList.TrimExcess();
        worldAI.theEnd = true;
        worldAI.hasBegun = false;
        worldAI.gameOver = false;

        randActivate.staticGo = true;
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        bossFight.bossGo = false;
        if (FindObjectOfType<pController>())
        {
            FindObjectOfType<pController>().gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
