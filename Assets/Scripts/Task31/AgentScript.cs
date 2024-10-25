using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }
}
