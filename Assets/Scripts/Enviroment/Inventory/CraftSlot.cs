using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public CraftRecipe itemRecipe;
    public Inventory inventory;


    void Start()
    {
        icon.sprite = itemRecipe.result.item.icon;
        labelText.text = itemRecipe.result.item.displayName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Se ha pulsado");
        inventory.CraftItem(itemRecipe);
    }

}
