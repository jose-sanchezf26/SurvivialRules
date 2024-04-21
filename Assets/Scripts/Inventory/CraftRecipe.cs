using UnityEngine;

[CreateAssetMenu]
public class CraftRecipe : ScriptableObject
{
    public CraftingIngredient[] requiredIngredients;
    public CraftingResult result;
}

[System.Serializable]
public class CraftingIngredient
{
    public ItemData item;
    public int amount;
}

[System.Serializable]
public class CraftingResult
{
    public ItemData item;
    public int amount;
}