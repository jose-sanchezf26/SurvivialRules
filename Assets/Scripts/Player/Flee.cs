using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform enemy;
    public float speed = 5f;

    private Rigidbody2D rb;
    public bool active = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (active)
        {
            // Vector2 fleeDirection = (transform.position - enemy.position).normalized;

            // float currentSpeed = Mathf.Min(speed, rb.velocity.magnitude);

            // rb.velocity = fleeDirection * currentSpeed;

            // Calculate flee direction (away from the enemy)
            Vector3 fleeDirection = (transform.position - enemy.position).normalized;

            // Calculate the new position for the player, ensuring it does not exceed the maximum distance
            Vector3 newPosition = transform.position + fleeDirection * speed * Time.deltaTime;

            // Update the player's position
            transform.position = newPosition;
        }
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }
}
