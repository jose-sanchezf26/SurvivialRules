using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    private bool canAttack = true;

    public void DoAttack(string enemy)
    {
        // Se puede realizar el ataque
        if (canAttack)
        {
            // Detectar si hay objetivos dentro del rango de ataque
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
            foreach (Collider2D hitCollider in hitColliders)
            {
                // Comprobar si el objeto dentro del rango es un enemigo
                if (hitCollider.GetComponent(enemy) != null)
                {
                    if (enemy != "Player")
                    {
                        // Hacer daño al enemigo
                        hitCollider.GetComponent<Health>().TakeDamage(damage);
                    }
                    else
                    {
                        hitCollider.GetComponent<Player>().TakeDamage(damage);
                    }
                }
            }

            // Después de atacar, iniciar el cooldown
            canAttack = false;
            Invoke("ResetAttack", attackCooldown);
        }
    }

    // Método para reiniciar la capacidad de ataque después del cooldown
    private void ResetAttack()
    {
        canAttack = true;
    }

    // Método para visualizar el rango de ataque en el editor de Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
