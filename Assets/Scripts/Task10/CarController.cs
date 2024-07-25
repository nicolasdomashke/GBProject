using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint axisPos;
    [SerializeField] private float rotationSpeed = 30f;
    private HingeJoint[] hinges;
    private float axisX = 0f;
    private float axisXTarget = 0f;
    void Awake()
    {
        hinges = GetComponentsInChildren<HingeJoint>();
    }

    void Update()
    {
        foreach (HingeJoint joint in hinges)
        {
            if (Input.GetKey("w"))
            {
                var mt = joint.motor;
                mt.targetVelocity = 1000;
                //mt.force = 300;
                joint.motor = mt;
            }
            else if (Input.GetKey("s"))
            {
                var mt = joint.motor;
                mt.targetVelocity = -100;
                //mt.force = 300;
                joint.motor = mt;
            }
            joint.useMotor = Input.GetKey("w") || Input.GetKey("s");
        }
        if (Input.GetKey("a"))
        {
            axisXTarget = 15f;
        }
        else if (Input.GetKey("d"))
        {
            axisXTarget = -15f;
        }
        else
        {
            axisXTarget = 0f;
        }
        axisX = Mathf.MoveTowards(axisX, axisXTarget, rotationSpeed * Time.deltaTime);

        Quaternion rotationX = new Quaternion();
        rotationX.eulerAngles = new Vector3(axisX, 0, 0);
        axisPos.targetRotation = rotationX;
    }
}
