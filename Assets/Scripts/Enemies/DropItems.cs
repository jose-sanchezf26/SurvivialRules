using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    // Método que añade objetos al inventario del jugador
    public void AddToInventory(ItemData itemData)
    {
        inventory.Add(itemData);
    }
}
