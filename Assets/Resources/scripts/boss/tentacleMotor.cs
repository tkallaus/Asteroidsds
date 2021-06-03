using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacleMotor : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private int pog;
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
    }
    
    void Update()
    {
        pog = Random.Range(70, 130);
        if (transform.localRotation.z < -.15)
        {
            motor.motorSpeed = -pog;
            hinge.motor = motor;
        }
        if (transform.localRotation.z > .15)
        {
            motor.motorSpeed = pog;
            hinge.motor = motor;
        }
    }
}
