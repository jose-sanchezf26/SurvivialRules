using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();


    private void OnEnable()
    {
        Stone.OnStoneCollected += Add;
        Wood.OnWoodCollected += Add;
    }

    private void OnDisable()
    {
        Stone.OnStoneCollected -= Add;
        Wood.OnWoodCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack(1);
            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}.");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {newItem.itemData.displayName} to the inventory for the first time.");
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack(1);
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

}