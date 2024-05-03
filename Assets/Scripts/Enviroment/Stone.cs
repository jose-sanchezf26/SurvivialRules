using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, ICollectible
{
    public static event HandleStoneCollected OnStoneCollected;
    public delegate void HandleStoneCollected(ItemData itemData);
    public ItemData stoneData;

    public void Collect()
    {
        FindAnyObjectByType<Player>().Speed = 2f;
        Destroy(gameObject);
        OnStoneCollected?.Invoke(stoneData);
    }
}
