using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : KitchenObjectHolder
{
    [SerializeField] private GameObject seletedCounter;

    public virtual void Interact(Player player)
    {
       
        
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
