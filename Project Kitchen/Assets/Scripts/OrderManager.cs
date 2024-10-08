using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set;}
    
    public event EventHandler OnRecipeSpawned;
    [SerializeField] private RecipeListSO recipeSOList;
    [SerializeField] private int orderMaxCount = 5;

    [SerializeField] private float orderRate = 2 ;


    private List<RecipeSO> orderRecipeSoList = new List<RecipeSO>();

    private float orderTime = 0;
    private bool isStartOrder = false;
    private int orderCount = 0;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        isStartOrder = true;
    }

    private void Update()
    {
        if (isStartOrder)
        {
            OrderUpdate();
        }

    } 

    private void OrderUpdate()
    {
        orderTime += Time.deltaTime;
        if (orderTime >= orderRate)
        {
            orderTime = 0; 
            OrderANewRecipe();
        } 
    }

    private void OrderANewRecipe()
    {
        if (orderCount >= orderMaxCount) return;

        orderCount++;
        int index = UnityEngine.Random.Range(0,recipeSOList.recipeSOList.Count);
        orderRecipeSoList.Add(recipeSOList.recipeSOList[index]);
        OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject )
    {
        RecipeSO correctRecipe = null;
        foreach(RecipeSO recipe in orderRecipeSoList)
        {
            if(IsCorrect(recipe,plateKitchenObject))
            {
                correctRecipe = recipe;
                break;
            }
        }

        if (correctRecipe == null)
        {
            print("Failure");
        }
        else
        {
            orderRecipeSoList.Remove(correctRecipe);
             print("Success"); 

        }

    }

    private bool IsCorrect(RecipeSO recipe,PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();
         
        if (list1.Count != list2.Count) return false;
        foreach(KitchenObjectSO kitchenObjectSO in list1)
        {
            if (list2.Contains(kitchenObjectSO) == false)
            {
                return false;
            }
        }
        return true;
    }

    public List<RecipeSO> GetOrderList()
    {
        return orderRecipeSoList;
    }

}
