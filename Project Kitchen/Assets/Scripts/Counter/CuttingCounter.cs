using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeListSO CuttingRecipeList;

    [SerializeField] private ProgressBarUI progressBarUI;

    [SerializeField] private CuttingCounterVisual cuttingCounterVisual;

    private int cuttingCount = 0;

    public override void Interact(Player player)
  {
        if (player.IsHaveKitchenObject())
        {//手上有食材
            if (IsHaveKitchenObject() == false)
            {//当柜台为空
                TransferKitchenObject(player, this);
            }
            else
            {

            }
        }
        else
        {//手上没有食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台 为空

            }
            else
            {//当前柜台 不为空
                TransferKitchenObject(this, player);
                progressBarUI.Hide();
            }
        }
  }
    
  public override void InteractOperate(Player player)
  {
        if ( IsHaveKitchenObject())
        {
            if (CuttingRecipeList.TryGetCuttingRecipe(GetKitchenObject().GetKitchenObjectSO(),
                out CuttingRecipe cuttingRecipe))
            {
                Cut();

                progressBarUI.UpdateProgress( (float)cuttingCount / cuttingRecipe.cuttingCountMax);

                if (cuttingCount == cuttingRecipe.cuttingCountMax)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipe.output.prefab);
                }

            }

        }
  }

    private void Cut()
    {
        cuttingCount++;
        cuttingCounterVisual.PlayCut();
    }
}
