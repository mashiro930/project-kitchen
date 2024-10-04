using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCounter : BaseCounter
{
    


    public override void Interact(Player player)
    {
        if (player.isHaveKitchenObject())
        {
            if (isHaveKitchenObject()== false)
            {
                TransferKitchenObject(player,this);
            } else 
            {

            }
        } else 
        {
            if (isHaveKitchenObject() == false)
            {

            } else 
            {
                TransferKitchenObject(this,player);

            }
        }
        
        
    }



    

    

}
