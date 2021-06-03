using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLaser : MonoBehaviour
{
    public ParticleSystem ps;
    public Transform laserActive;
    private float timer;
    public float maxTimer;
    public float startTimerOffset;
    private Outline chargeColor;
    private bool particleSwitch;
    public float direction;
    public float maxScale;
    public float scaleSpeed;
    private bool scaling;
    private bool directionSwitch;
    private Quaternion OGRotation;
    private void Start()
    {
        scaling = false;
        chargeColor = gameObject.GetComponent<Outline>();
        particleSwitch = true;
        timer = maxTimer + startTimerOffset;
        directionSwitch = false;
        OGRotation = transform.rotation;
    }
    private void Update()
    {
        if (bossFight.bossGo)
        {
            timer -= Time.deltaTime;
        }
        else if (worldAI.bossTime)
        {
            transform.rotation = OGRotation;
            timer = maxTimer + startTimerOffset;
            laserActive.localScale = new Vector3(43, 0, 1);
            scaling = false;
            particleSwitch = true;
            directionSwitch = false;
        }
    }
    private void FixedUpdate()
    {
        if (timer < -5f)
        {
            particleSwitch = true;
            scaling = false;
            laserActive.localScale -= new Vector3(0, 0.1f, 0);
            if (laserActive.localScale.y <= 0)
            {
                timer = maxTimer;
                directionSwitch = !directionSwitch;
                laserActive.localScale = new Vector3(43, 0, 1);
            }
        }
        else if (timer < 0f)
        {
            chargeColor.OutlineColor = Color.Lerp(chargeColor.OutlineColor, Color.green, Time.deltaTime);
            if (laserActive.localScale.y < 0.9 && !scaling)
            {
                laserActive.localScale += new Vector3(0, 0.1f, 0);
            }
            else
            {
                scaling = true;
            }
            if (scaling)
            {
                float scale = maxScale * Mathf.Sin(Time.time * scaleSpeed);
                laserActive.localScale = new Vector3(43, scale + 0.95f, 1);
            }
            if (!directionSwitch)
            {
                transform.Rotate(0, 0, direction);
            }
            else
            {
                transform.Rotate(0, 0, -direction);
            }
            
            ps.Clear();
        }
        else if (timer < 3f)
        {
            if (particleSwitch)
            {
                ps.Play();
                particleSwitch = false;
            }
            chargeColor.OutlineColor = Color.Lerp(chargeColor.OutlineColor, Color.red, Time.deltaTime);
        }
    }
}
