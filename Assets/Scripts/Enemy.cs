using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Vector3 charDistance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 2)
        {
            agent.destination = player.transform.position;
            agent.speed = 3.5f;
            //agent.isStopped = false;
        }            
        else
        {
            agent.speed = 0;
            //agent.isStopped = true;
        }
           
    }
}
