using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInit : MonoBehaviour
{
    private SliderJoint2D sliderJoint2D;
    void Awake()
    {
        sliderJoint2D = GetComponent<SliderJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        JointMotor2D jointMotor2D = sliderJoint2D.motor;
        jointMotor2D.motorSpeed = -1.3f;
        sliderJoint2D.motor = jointMotor2D;
    }
}
