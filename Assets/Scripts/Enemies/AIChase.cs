using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// Clase que contiene comportamiento de movimiento de un enemigo que sigue al jugador
public class AIChase : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed;

    private float distance;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SetSpeed(float s)
    {
        agent.speed = s;
    }

    void Update()
    {
        if (targetPosition != Vector2.zero)
        {
            agent.SetDestination(targetPosition);
        }
        else
        {
            agent.ResetPath();
        }
    }
}
