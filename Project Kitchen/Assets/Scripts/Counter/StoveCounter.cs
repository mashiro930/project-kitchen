 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingReceipeListSO fryingReceipeList;
    [SerializeField] private FryingReceipeListSO burningReceipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
    [SerializeField] private AudioSource sound;

    public enum StoveState
    {
        Idle,
        Frying,
        Burning,
    }

    private FryingReceipe fryingReceipe;
    private float fryingTime = 0;
    private StoveState state = StoveState.Idle;
    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {
            if (IsHaveKitchenObject()== false)
            {
                if (fryingReceipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(),out FryingReceipe fryingReceipe))
                {
                    TransferKitchenObject(player,this);
                    StartFrying(fryingReceipe);

                } else if(burningReceipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(),out FryingReceipe burningRecipe))
                {
                    TransferKitchenObject(player,this);
                    StartBurning(burningRecipe);

                }
               
            } else 
            { 

            }
        } else 
        {
            if (IsHaveKitchenObject() == false)
            {

            } else 
            {
                TurnToIdle(); 
                TransferKitchenObject(this,player);

            }
        }
    }

    private void Update()
    {
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTime += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTime / fryingReceipe.fryingTimne);
                if (fryingTime >= fryingReceipe.fryingTimne)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingReceipe.output.prefab);
                    
                    burningReceipeList.TryGetFryingRecipe(GetKitchenObject().GetKitchenObjectSO(),
                        out FryingReceipe newFryingReceipe);
                    StartBurning(newFryingReceipe);

                }
                break;
            case StoveState.Burning:
                fryingTime += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTime / fryingReceipe.fryingTimne);
                if (fryingTime >= fryingReceipe.fryingTimne)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingReceipe.output.prefab);
                    TurnToIdle(); 
                }
                break;
            default:
                break; 
        }
    }
    private void StartFrying(FryingReceipe fryingReceipe)
    {
        fryingTime = 0;
        this.fryingReceipe = fryingReceipe;
        state = StoveState.Frying;
        stoveCounterVisual.ShowStoveEffect();
        sound.Play();
     
    }

    private void StartBurning(FryingReceipe fryingReceipe)
    {
        if (fryingReceipe == null)
        {
            Debug.LogWarning("Unable to get Burning receipe");
            TurnToIdle();
            return;

        }

        stoveCounterVisual.ShowStoveEffect();
        fryingTime = 0;
        this.fryingReceipe = fryingReceipe;
        state = StoveState.Burning;
    }

    private void TurnToIdle()
    {
        progressBarUI.Hide();
        state = StoveState.Idle;
        stoveCounterVisual.HideStoveEffect();
        sound.Pause();
    }
 
}
