using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject orderManager;
    [SerializeField] private OrderManager orderManagerS;
    // Start is called before the first frame update
    void Start()
    {
        orderManagerS = orderManager.GetComponent<OrderManager>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "Current Score: " + orderManagerS.successDeliveryCount;
    }
}
