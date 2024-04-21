using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TextMeshProUGUI labelText;
    public Image itemsRequiredImage;
    public TextMeshProUGUI itemsRequired;
    public CraftRecipe itemRecipe;
    public Inventory inventory;


    void Start()
    {
        icon.sprite = itemRecipe.result.item.icon;
        labelText.text = itemRecipe.result.item.displayName;
        itemsRequiredImage.enabled = false;
        itemsRequired.enabled = false;
        itemsRequired.text = ItemsRequiredToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Se ha pulsado");
        inventory.CraftItem(itemRecipe);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemsRequiredImage.enabled = true;
        itemsRequired.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemsRequiredImage.enabled = false;
        itemsRequired.enabled = false;
    }

    // Devuelve una cadena con los objetos necesarios para hacer el objeto, junto con su cantidad
    private string ItemsRequiredToString()
    {
        string items = "";

        foreach (var item in itemRecipe.requiredIngredients)
        {
            items += $"{item.amount} {item.item.displayName}\n";
        }

        return items;
    }


}
