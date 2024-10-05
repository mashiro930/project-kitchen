using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private PlateCompleteVisual plateCompleteVisual;
    [SerializeField] private List<KitchenObjectSO> validFoods = new List<KitchenObjectSO>();
    [SerializeField] private KitchenObjectGridUI gridUI;
    private List<KitchenObjectSO> foods = new List<KitchenObjectSO>();
    // Start is called before the first frame update
    public bool AddKitchenObjectSO(KitchenObjectSO KitchenObjectSO) {
        if (!foods.Contains(KitchenObjectSO) && validFoods.Contains(KitchenObjectSO)) {
            plateCompleteVisual.ShowKitchenObject(KitchenObjectSO);
            foods.Add(KitchenObjectSO);
            gridUI.ShowKitchenObjectUI(KitchenObjectSO);


            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
