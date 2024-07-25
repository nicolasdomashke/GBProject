using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public class TrajectoryBehaviour : MonoBehaviour
{
    public Vector3 activePoint;
    private Transform [] trajectory;
    private int currentpoint = 0;
    

    void Awake()
    {
        trajectory = GetComponentsInChildren<Transform>();
        trajectory = trajectory.Skip(1).ToArray();
        activePoint = trajectory[currentpoint].transform.position;
    }
    public void NextPoint()
    {
        currentpoint = (currentpoint + 1) % trajectory.Length;
        activePoint = trajectory[currentpoint].transform.position;
    }
}
