using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingReceipeListSO : ScriptableObject 
{
    public List<FryingReceipe> list;

    public bool TryGetFryingRecipe(KitchenObjectSO input,out FryingReceipe fryingReceipe)
    {
        foreach (FryingReceipe recipe in list)
        {
            if (recipe.input == input)
            {
                fryingReceipe = recipe;return true;
            }
        }
        fryingReceipe = null;
        return false;
    } 

}
[Serializable]
public class FryingReceipe
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimne; 
}