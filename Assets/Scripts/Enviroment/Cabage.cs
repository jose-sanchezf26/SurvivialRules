using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabage : MonoBehaviour
{
    // Distancia a la que necesita estar el jugador para descansar
    public float restDistance = 1f;
    private float cooldown = 20f;
    private float lastUsedTime = -Mathf.Infinity;

    public bool CanRest()
    {
        return Time.time - lastUsedTime >= cooldown;
    }

    public void UpdateCoolDown()
    {
        lastUsedTime = Time.time;
    }
}
