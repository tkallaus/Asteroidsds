using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserAI : MonoBehaviour
{
    public ParticleSystem ps;
    public Transform laserActive;
    private float timer;
    public float maxTimer;
    private Outline chargeColor;
    private bool particleSwitch;
    private void Start()
    {
        chargeColor = gameObject.GetComponent<Outline>();
        particleSwitch = true;
        timer = maxTimer;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < -1f)
        {
            particleSwitch = true;
            laserActive.localScale -= new Vector3(0, 0.1f, 0);
            if(laserActive.localScale.y <= 0)
            {
                timer = maxTimer;
                laserActive.localScale = new Vector3(34, 0, 1);
            }
        }
        else if (timer < 0f)
        {
            chargeColor.OutlineColor = Color.Lerp(chargeColor.OutlineColor, Color.green, 3 * Time.deltaTime);
            if(laserActive.localScale.y < 0.9)
            {
                laserActive.localScale += new Vector3(0, 0.1f, 0);
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
            chargeColor.OutlineColor = Color.Lerp(chargeColor.OutlineColor, Color.red, 1 * Time.deltaTime);
        }
    }
}
