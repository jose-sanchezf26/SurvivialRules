using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertDirection : MonoBehaviour
{
    private Vector2 lastPosition;
    private Vector2 currentPosition;

    // Update is called once per frame
    void Update()
    {

        //Se detecta si hay movimiento
        currentPosition = transform.position;

        // Calcula la dirección del movimiento
        float direction = Mathf.Sign(currentPosition.x - lastPosition.x);
        if (direction > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        lastPosition = currentPosition;
    }
}
