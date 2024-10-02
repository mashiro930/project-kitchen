using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject seletedCounter;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    [SerializeField] private bool testing = false;

    [SerializeField] private ClearCounter transferTargetCounter;

    

    private void Update()
    {
        if( testing && Input.GetMouseButtonDown(0))
        {
            TransferKitchenObject(this, transferTargetCounter);
        }
    }

    public void Interact()
    {
        if (GetKitchenObject() == null)
        {
            KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectSO.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
            SetKitchenObject(kitchenObject);
        } else
        {
            TransferKitchenObject(this, Player.Instance);
        }
        
    }

    public void SelectCounter()
    {
        seletedCounter.SetActive(true);
    }

    public void CancelSelect()
    {
        seletedCounter.SetActive(false);
    }

    

    

}
