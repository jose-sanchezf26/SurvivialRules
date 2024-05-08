using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class Detector : MonoBehaviour
{
    public float visionRadius = 5f;
    public float detectionRadius = 5f;
    // Número de rayos para simular el círculo
    public int raysCount = 100;
    public LayerMask detectionLayer;
    //Lista que almacena los objetos que ha detectado el jugador
    public List<GameObject> detectedObjects = new List<GameObject>();

    public void DetectObjectsInVision()
    {
        // float angleIncrement = 360f / raysCount; // Incremento angular entre rayos

        // for (int i = 0; i < raysCount; i++)
        // {
        //     float angle = i * angleIncrement;
        //     Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, visionRadius, detectionLayer);

        //     if (hit.collider != null)
        //     {
        //         GameObject detectedObject = hit.collider.gameObject;

        //         if (!detectedObjects.Contains(detectedObject))
        //         {
        //             detectedObjects.Add(detectedObject);
        //         }
        //         Debug.Log("Se ha detectado un objeto");

        //     }
        // }

        // Detecta todos los colliders dentro del círculo de detección
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);

        // Nuevo HashSet temporal para almacenar los objetos detectados en este frame
        List<GameObject> newDetectedObjects = new List<GameObject>();

        // Itera sobre los colliders detectados
        foreach (Collider2D collider in colliders)
        {
            // Accede al GameObject asociado con el collider
            GameObject detectedObject = collider.gameObject;

            // Si el objeto no estaba previamente en la lista, añadirlo al HashSet temporal
            if (!detectedObjects.Contains(detectedObject) && detectedObject != null)
            {
                newDetectedObjects.Add(detectedObject);
                Debug.Log("Se ha detectado un nuevo objeto: " + detectedObject.name);
            }
        }

        // Actualizar la lista de objetos detectados
        if (newDetectedObjects.Count > 0)
        {
            detectedObjects.Clear();
            detectedObjects = newDetectedObjects;
        }

    }

    // Método que comprueba si hay un objeto en concreto detectado
    // public bool ObjectDetected(string objectd)
    // {
    //     if (detectedObjects.Count > 0)
    //     {
    //         foreach (GameObject detectedObject in detectedObjects)
    //         {
    //             if (detectedObject != null)
    //             {
    //                 if (detectedObject.GetComponent(objectd) != null)
    //                 {
    //                     return true;
    //                 }
    //             }

    //         }
    //     }
    //     return false;
    // }

    // Método que comprueba si hay un objeto en concreto detectado
    public bool ObjectDetected(string objectType)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            DetectableName detectableName = detectedObject.GetComponent<DetectableName>();
            if (detectedObject != null && detectableName != null)
            {
                if (detectableName.displayName == objectType)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Método que devuelve la posición de un objeto detectado, según su nombre de clase
    public Vector2 DetectedPosition(string name)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            DetectableName detectableName = detectedObject.GetComponent<DetectableName>();
            if (detectedObject != null && detectableName != null)
            {
                if (detectableName.displayName == name)
                {
                    return detectedObject.transform.position;
                }
            }
        }
        return Vector2.zero;
    }

    // Devuelve el transform del objeto detectado
    public Transform DetectedTransform(string name)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            if (detectedObject != null && detectedObject.GetComponent(name) != null)
            {
                return detectedObject.transform;
            }
        }
        return null;
    }

    // Método para devolver una lista de objetos detectados en el momento
    public List<DetectableName> DetectableNames()
    {
        List<DetectableName> detectableNames = new List<DetectableName>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, detectionLayer);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            DetectableName detectableName = detectedObject.GetComponent<DetectableName>();
            if (detectedObject != null && detectableName != null)
            {
                detectableNames.Add(detectableName);
            }
        }
        return detectableNames;
    }


    // Método para visualizar el campo de visión en la escena de Unity
    void OnDrawGizmos()
    {
        // Gizmos.color = Color.yellow;
        // float angleIncrement = 360f / raysCount;

        // for (int i = 0; i < raysCount; i++)
        // {
        //     float angle = i * angleIncrement;
        //     Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

        //     Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * visionRadius);
        // }

        // Dibuja un círculo de detección en el editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}