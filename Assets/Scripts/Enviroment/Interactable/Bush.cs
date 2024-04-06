using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    public Sprite berries;
    public Sprite nberries;
    public ItemData berrie;
    public int timeBerries = 5;
    private bool touched;
    private SpriteRenderer sprite;
    private Inventory inventory;

    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = berries;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador ha tocado el arbusto y si el arbusto no ha sido tocado recientemente
        if (other.gameObject.CompareTag("Player") && !touched)
        {
            // Cambiar el sprite del arbusto
            sprite.sprite = nberries;

            // Dar el objeto al jugador
            inventory.Add(berrie);

            // Marcar el arbusto como ya tocado y cambia su layer para no ser detectable
            gameObject.layer = 0;
            touched = true;

            // Esperar a que se regeneren las bayas
            StartCoroutine(ReiniciarArbusto());
        }
    }

    IEnumerator ReiniciarArbusto()
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(timeBerries);

        // Cambiar el sprite del arbusto de vuelta al original
        sprite.sprite = berries;

        // Permitir que el arbusto dé otro objeto
        touched = false;
        gameObject.layer = 3;
    }
}
