using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仓库类柜台
public class ContainerCounter : BaseCounter
{   
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ContainerCounterVisual containerCounterVisual;

    public override void Interact(Player player)
    {
        if (player.isHaveKitchenObject()) return;
        
        CreateKitchenObject(kitchenObjectSO.prefab);
    
        TransferKitchenObject(this, player);

        containerCounterVisual.PlayOpen();
       
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetHoldPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

}
