using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, ICollectible
{
    public static event HandleWoodCollected OnWoodCollected;
    public delegate void HandleWoodCollected(ItemData itemData);
    public ItemData WoodData;

    public void Collect()
    {
        FindAnyObjectByType<Player>().Speed = 2f;
        Destroy(gameObject);
        OnWoodCollected?.Invoke(WoodData);
    }
}