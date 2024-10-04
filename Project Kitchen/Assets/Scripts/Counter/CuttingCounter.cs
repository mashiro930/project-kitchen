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
        {//������ʳ��
            if (IsHaveKitchenObject() == false)
            {//����̨Ϊ��
                TransferKitchenObject(player, this);
            }
            else
            {

            }
        }
        else
        {//����û��ʳ��
            if (IsHaveKitchenObject() == false)
            {//��ǰ��̨ Ϊ��

            }
            else
            {//��ǰ��̨ ��Ϊ��
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
