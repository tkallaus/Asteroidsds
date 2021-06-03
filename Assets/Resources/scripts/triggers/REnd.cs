using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnd : MonoBehaviour
{
    public Material green;
    public Renderer ind;
    public GameObject RBlock;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        finalDoor.doorLock += 1;
        RBlock.SetActive(true);
        ind.material = green;
        worldAI.stage3Transition = true;
        worldAI.gameOver = true;
        worldAI.spawnPoint = new Vector2(0, 0);
        gameObject.SetActive(false);
    }
}
