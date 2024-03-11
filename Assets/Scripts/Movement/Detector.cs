using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Detector : MonoBehaviour
{
    public float visionRadius = 5f;
    // Número de rayos para simular el círculo
    public int raysCount = 100;
    public LayerMask detectionLayer;
    //Lista que almacena los objetos que ha detectado el jugador
    public List<GameObject> detectedObjects;

    public void DetectObjectsInVision()
    {
        float angleIncrement = 360f / raysCount; // Incremento angular entre rayos

        for (int i = 0; i < raysCount; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRadius, detectionLayer);

            if (hit.collider != null)
            {
                GameObject detectedObject = hit.collider.gameObject;

                if (!detectedObjects.Contains(detectedObject))
                {
                    detectedObjects.Add(detectedObject);
                }
                Debug.Log("Se ha detectado un objeto");

            }
        }
    }

    // Método que comprueba si hay un objeto en concreto detectado
    public bool ObjectDetected(string objectd)
    {
        foreach (GameObject detectedObject in detectedObjects)
        {
            if (detectedObject.GetComponent(objectd) != null)
            {
                return true;
            }
        }
        return false;
    }

    // Método para visualizar el campo de visión en la escena de Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float angleIncrement = 360f / raysCount;

        for (int i = 0; i < raysCount; i++)
        {
            float angle = i * angleIncrement;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * visionRadius);
        }
    }
}