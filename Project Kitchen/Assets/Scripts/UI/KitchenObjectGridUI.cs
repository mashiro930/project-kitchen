using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField] private KitchenObjectIconUI kitchenObjectIcon;
    private void Start()
    {
        kitchenObjectIcon.Hide();
    }
    public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO) {
        KitchenObjectIconUI newUI = GameObject.Instantiate(kitchenObjectIcon);
        newUI.transform.SetParent(transform);
        newUI.transform.localScale = Vector3.one*2;
        newUI.Show(kitchenObjectSO.sprite);
    }
}
