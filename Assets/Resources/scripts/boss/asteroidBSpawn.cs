using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidBSpawn : MonoBehaviour
{
    private float timer;
    public float maxTimer;
    public GameObject ast;
    private void Start()
    {
        timer = maxTimer;
    }
    void Update()
    {
        if (bossFight.bossGo)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                timer = maxTimer;
                switch (Random.Range(0,4))
                {
                    case 0:
                        ast = Instantiate(Resources.Load("prefabs/obstacles/asteroidS" + Random.Range(0, 2)), transform.position, Quaternion.identity) as GameObject;
                        break;
                    case 1:
                        ast = Instantiate(Resources.Load("prefabs/obstacles/asteroidM" + Random.Range(0, 3)), transform.position, Quaternion.identity) as GameObject;
                        break;
                    case 2:
                        ast = Instantiate(Resources.Load("prefabs/obstacles/asteroidL" + Random.Range(0, 1)), transform.position, Quaternion.identity) as GameObject;
                        break;
                    case 3:
                        ast = Instantiate(Resources.Load("prefabs/obstacles/asteroidAlien"), transform.position, Quaternion.identity) as GameObject;
                        break;
                    default:
                        Debug.Log("What? How?");
                        break;
                }
                ast.GetComponent<asteroidAI>().randSpawned = false;
            }
        }
    }
}
