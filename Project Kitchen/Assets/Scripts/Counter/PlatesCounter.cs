using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float timer = 0;
    private float spawnRate = 3;
    public KitchenObjectSO Plate;
    private List<KitchenObject> list;
    private void Start()
    {
        
        list = new List<KitchenObject>();
    }
    // Start is called before the first frame update 
    public override void Interact(Player player)
    {

        if (!player.IsHaveKitchenObject()&& list.Count>0) {
            player.AddKitchenObject(list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);
        }
    }
    // Update is called once per frame
     void Update()
    {
        if (list.Count < 5)
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                KitchenObject NewOb = GameObject.Instantiate(Plate.prefab, GetHoldPoint()).GetComponent<KitchenObject>();
                NewOb.transform.localPosition = Vector3.zero + Vector3.up * 0.1f * list.Count;
                list.Add(NewOb); 
                timer = 0;
            }
        }
    }
}
