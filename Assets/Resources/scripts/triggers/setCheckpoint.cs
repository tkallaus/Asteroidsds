using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCheckpoint : MonoBehaviour
{
    public Vector2 loc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            worldAI.spawnPoint = loc;
        }
    }
}
