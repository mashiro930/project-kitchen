using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score01;
    [SerializeField] private TextMeshProUGUI score02;

    // Start is called before the first frame update
    void Start()
    {
        score01.SetText("Highest Score: " + GameState.GetStarNumber01().ToString());
        score02.SetText("Highest Score: " + GameState.GetStarNumber02().ToString());
    }
}
