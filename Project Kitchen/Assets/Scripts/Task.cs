using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int number;
    public int current;
    public List<RecipeSO> orderRecipeSoList = new List<RecipeSO>();
    public TextMeshProUGUI textMesh;
    public GameObject orderManager;
    [SerializeField] private OrderManager orderManagerS;
    public int extraScore;
    public bool finish;
    // Start is called before the first frame update
    void Start()
    {
        orderManagerS = orderManager.GetComponent<OrderManager>();
        textMesh.text = current + "/" + number + " (+" + extraScore + ")";
        finish = false;
    }

    // Update is called once per frame
    public int checkMission(RecipeSO Order) { 
        if (orderRecipeSoList.Contains(Order) && !finish)
        {
            current++;
            textMesh.text = current + "/" + number + " (+" + extraScore +")";
            if (current == number) { 
                finish = true;
                SetTextAlpha(0.5f);
                //gameObject.SetActive(false);
                return extraScore;
            }
        }
        return 0;
    }
    public void SetTextAlpha(float alpha)
    {
        Color color = textMesh.color;
        color.a = Mathf.Clamp01(alpha); // 确保alpha值在0到1之间
        textMesh.color = color;
    }
}
