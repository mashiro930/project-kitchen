using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCounter : BaseCounter
{



    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (player.GetKitchenObject().CompareTag("Plate"))
            {
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject().GetComponent<PlateKitchenObject>();
                    if (plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO())) {
                        DestroyKitchenObject();
                    }
                    
                }
            }
            else {
                if (IsHaveKitchenObject() == false)
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    if (GetKitchenObject().CompareTag("Plate")) {
                        PlateKitchenObject plateKitchenObject = GetKitchenObject().GetComponent<PlateKitchenObject>();
                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject();
                        }
                    }
                }
            }
            
        }
        else
        {
            if (IsHaveKitchenObject() == false)
            {

            }
            else
            {
                TransferKitchenObject(this, player);

            }
        }


    }







}