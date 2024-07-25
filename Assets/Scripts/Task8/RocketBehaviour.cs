using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    [SerializeField] private TrajectoryBehaviour trajectoryBehaviour;
    [SerializeField] private Transform targetRocket;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 100f;

    void Start() 
    {
        RelocateTarget(trajectoryBehaviour.activePoint);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, trajectoryBehaviour.activePoint, moveSpeed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (transform.position == trajectoryBehaviour.activePoint)
        {
            trajectoryBehaviour.NextPoint();
            RelocateTarget(trajectoryBehaviour.activePoint);
        }
    }
    private void RelocateTarget(Vector3 endPoint)
    {
        transform.LookAt(endPoint);
        targetRocket.position = endPoint;
        targetRocket.rotation = transform.rotation;
        float timeToTarget = Vector3.Distance(transform.position, endPoint) / moveSpeed;
        targetRocket.Rotate(0, 0, rotationSpeed * timeToTarget);
    }
}
