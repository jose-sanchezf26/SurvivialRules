using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public ItemData itemToCook;
    public ItemData CookedItem;
    private Inventory inventory;
    public float cookDistance = 1;
    public float cookTime = 10;

    public void SetItem(ItemData item)
    {
        itemToCook = item;
    }
    void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void Cook()
    {
        StartCoroutine(Delay());
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(cookTime);
        inventory.Add(CookedItem);
        FindAnyObjectByType<Player>().isCooking = false;
    }


}