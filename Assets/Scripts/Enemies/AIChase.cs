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

    void Update()
    {
        // distance = Vector2.Distance(transform.position, targetPosition);
        // Vector2 direction = targetPosition - transform.position;
        // direction.Normalize();
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
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
