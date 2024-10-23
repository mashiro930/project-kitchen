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
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeSOList;
    [SerializeField] private int orderMaxCount = 30;
    [SerializeField] private int maxActiveOrders = 5; // Limit the number of active orders to 5
    [SerializeField] private float orderRate = 3; // Adjust the rate if needed

    private List<RecipeSO> orderRecipeSoList = new List<RecipeSO>();

    private float orderTime = 0;
    private bool isStartOrder = false;
    private int orderCount = 0; // Total orders spawned
    private int activeOrderCount = 0; // Currently active orders
    private int successDeliveryCount = 0;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            StartSpawnOrder();
        }
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
        if (activeOrderCount < maxActiveOrders && orderCount < orderMaxCount) // Check active orders limit
        {
            orderTime += Time.deltaTime;
            if (orderTime >= orderRate)
            {
                orderTime = 0;
                OrderANewRecipe();
            }
        }
    }

    private void OrderANewRecipe()
    {
        if (orderCount >= orderMaxCount) return; // Don't spawn more than orderMaxCount

        orderCount++; // Increase total orders spawned
        activeOrderCount++; // Increase active orders count

        int index = UnityEngine.Random.Range(0, recipeSOList.recipeSOList.Count);
        orderRecipeSoList.Add(recipeSOList.recipeSOList[index]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO correctRecipe = null;
        foreach (RecipeSO recipe in orderRecipeSoList)
        {
            if (IsCorrect(recipe, plateKitchenObject))
            {
                correctRecipe = recipe;
                break;
            }
        }

        if (correctRecipe == null)
        {
            print("Order Failure");
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            orderRecipeSoList.Remove(correctRecipe);
            OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
            successDeliveryCount++;
            activeOrderCount--; // Reduce active orders after successful delivery
            print("Order Success");

            // Spawn a new order if there are still remaining orders to be spawned
            if (orderCount < orderMaxCount)
            {
                OrderANewRecipe();
            }
        }
    }

    private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.kitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();

        if (list1.Count != list2.Count) return false;
        foreach (KitchenObjectSO kitchenObjectSO in list1)
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

    public void StartSpawnOrder()
    {
        isStartOrder = true;
    }

    public int GetSuccessDeliveryCount()
    {
        return successDeliveryCount;
    }
}
