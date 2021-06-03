using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pController : MonoBehaviour
{
    public float rotSpeed;
    public float thrust;

    private float hori;
    private float vert;
    private Rigidbody2D rig;

    public float maxSpeed;

    public Rigidbody2D boolet;
    public Transform booletSpawn;
    public float booletSpeed = 1f;
    private float reloadSpeed;

    public GameObject thrustE;

    private AudioSource[] sounds; //0 is laser, 1 is thrust.

    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        reloadSpeed = 0.2f;
        sounds = GetComponents<AudioSource>();
    }
    private void Update()
    {
        if (worldAI.screenWrapActive)
        {
            ScreenWrap();
        }
        if (worldAI.hasBegun)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && reloadSpeed <= 0)
            {
                Rigidbody2D cloneB = Instantiate(boolet, booletSpawn.position, Quaternion.identity) as Rigidbody2D;
                cloneB.AddForce(transform.up * booletSpeed);
                sounds[0].Play();
                reloadSpeed = 0.2f;
            }
            reloadSpeed -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.P) && !worldAI.paused)
            {
                Explode();
            }
        }
        if (worldAI.bossTime)
        {
            thrust = 11;
            maxSpeed = 10;
            booletSpeed = 600;
        }
        else
        {
            thrust = 7;
            maxSpeed = 6;
            booletSpeed = 500;
        }
    }
    void FixedUpdate()
    {
        hori = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        rig.angularVelocity = -hori * rotSpeed;
        if (vert > 0)
        {
            rig.AddForce(transform.up * vert * thrust);
            thrustE.SetActive(true);
            float scale = 0.5f * Mathf.Sin(Time.time * 10);
            thrustE.transform.localScale = new Vector3(0.25f, (scale + 2f) * vert, 1);
            sounds[1].volume = 0.02f;
        }
        else if (vert < 0)
        {
            rig.velocity *= 0.96f;
            if (rig.velocity.magnitude < 0.6f)
            {
                rig.velocity *= 0f;
            }
            thrustE.SetActive(false);
            sounds[1].volume = 0f;
        }
        else
        {
            thrustE.SetActive(false);
            sounds[1].volume = 0f;
        }

        if (rig.velocity.magnitude > maxSpeed)
        {
            rig.velocity = rig.velocity.normalized * maxSpeed;
        }
    }

    void ScreenWrap()
    {
        Vector3 newPos = transform.position;

        if (!worldAI.bossTime)
        {
            if (newPos.x > worldAI.HalfScreenWidth + (worldAI.HalfScreenWidth*2)*worldAI.camGrid.x)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 11)
        {
            Explode();
        }
    }

    private void Explode()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            Rigidbody2D cloneB = Instantiate(boolet, transform.position, Quaternion.identity) as Rigidbody2D;
            cloneB.AddForce(Random.insideUnitCircle.normalized * booletSpeed);
        }
        AudioSource.PlayClipAtPoint(Resources.Load("sounds/explode") as AudioClip, transform.position, 0.5f);
        worldAI.gameOver = true;
    }
}
