using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score01;
    [SerializeField] private TextMeshProUGUI score02;
    public Image levelStar1;
    public Image levelStar2;
    public Image levelStar3;
    public Image leve2Star1;
    public Image leve2Star2;
    public Image leve2Star3;

    // Start is called before the first frame update
    void Start()
    {
        score01.SetText("Highest Score: " + GameState.GetStarNumber01().ToString());
        score02.SetText("Highest Score: " + GameState.GetStarNumber02().ToString());
    }
    private void Update()
    {
        if (GameState.GetStarNumber01() == 3)
        {
            levelStar3.gameObject.SetActive(true);
        }
        else if (GameState.GetStarNumber01() == 2)
        {
            levelStar2.gameObject.SetActive(true);
        }
        else if (GameState.GetStarNumber01() == 1)
        {
            levelStar1.gameObject.SetActive(true);
        }
        if (GameState.GetStarNumber02() == 3)
        {
            leve2Star3.gameObject.SetActive(true);
        }
        else if (GameState.GetStarNumber02() == 2)
        {
            leve2Star2.gameObject.SetActive(true);
        }
        else if (GameState.GetStarNumber02() == 1)
        {
            leve2Star1.gameObject.SetActive(true);
        }

    }
}
