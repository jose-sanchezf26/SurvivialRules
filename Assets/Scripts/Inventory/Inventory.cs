using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChanged;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();
    public ItemData campfireData;
    public ItemData cabageData;

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
            OnInventoryChanged?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {newItem.itemData.displayName} to the inventory for the first time.");
            OnInventoryChanged?.Invoke(inventory);
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
            OnInventoryChanged?.Invoke(inventory);
        }
    }


    // Obtiene el ItemData de un objeto del inventario según su nombre
    public ItemData GetItemData(string itemName)
    {
        foreach (var par in itemDictionary)
        {
            if (par.Key.displayName == itemName)
            {
                return par.Key;
            }
        }
        return null;
    }

    // Método que comprueba si el inventario tiene un objeto determinado
    public bool HasItemData(ItemData itemData)
    {
        return itemDictionary.TryGetValue(itemData, out InventoryItem item);
    }

    //Método para saber si hay un objeto en el inventario
    public bool HasItem(string item)
    {
        foreach (var pair in itemDictionary)
        {
            if (pair.Key.displayName == item)
            {
                return true;
            }
        }
        return false;
    }

    // Método para craftear objetos
    public void CraftItem(CraftRecipe recipe)
    {
        bool canDoIt = true;

        // Comprueba que tiene todos los ingredientes
        foreach (var ingredient in recipe.requiredIngredients)
        {
            if (!itemDictionary.ContainsKey(ingredient.item) || itemDictionary[ingredient.item].stackSize < ingredient.amount)
            {
                canDoIt = false;
                break;
            }
        }

        if (canDoIt)
        {
            // Elimina los items del inventario
            foreach (var ingredient in recipe.requiredIngredients)
            {
                for (int i = 0; i < ingredient.amount; i++)
                {
                    Remove(ingredient.item);
                }
            }

            // Añade el nuevo item al inventario
            for (int i = 0; i < recipe.result.amount; i++)
            {
                Add(recipe.result.item);
            }
            Debug.Log("Objeto " + recipe.result.item.displayName + " crafteado");
        }
        else
        {
            Debug.Log("Objeto " + recipe.result.item.displayName + " no ha podido ser crafteado");
        }
    }

}