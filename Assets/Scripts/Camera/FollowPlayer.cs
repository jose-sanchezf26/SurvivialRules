using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Jugador al que sigue la cámara
    public Transform player;
    // Suavidad de la cámara
    public float smoothing = 5f; 

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calcula la posición deseada de la cámara
            Vector3 targetposition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Mueve suavemente la cámara hacia la posición deseada
            transform.position = Vector3.Lerp(transform.position, targetposition, smoothing * Time.fixedDeltaTime);
        }
    }
}
