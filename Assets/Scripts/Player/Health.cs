using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public ItemData drop;
    public int nDrop;
    public AttackType attackType;
    private float currentHealth;
    private Inventory inventory;

    void Start()
    {
        currentHealth = maxHealth;
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void TakeDamage(int damage, AttackType type)
    {
        if (type == attackType)
        {
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto");
        for (int i = 0; i < nDrop; i++)
        {
            inventory.Add(drop);
        }
        Destroy(gameObject);
    }
}
