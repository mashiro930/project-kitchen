using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
     [Serializable] public class KitchenObjectSO_Model {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject model;
    }
    [SerializeField] private List<KitchenObjectSO_Model> modelDic;
    // Start is called before the first frame update
    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
      foreach (KitchenObjectSO_Model models in modelDic)
        {
            if (models.kitchenObjectSO == kitchenObjectSO) { 
                models.model.SetActive(true);
                
                
                return;

            }
        }
    }
}
