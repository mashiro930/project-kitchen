using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private GameObject king01;
    [SerializeField] private GameObject helmet01;
    [SerializeField] private GameObject viking01;
    [SerializeField] private GameObject king02;
    [SerializeField] private GameObject leaf02;
    [SerializeField] private GameObject sprout02;

    // Start is called before the first frame update
    void Start()
    {
        king01.SetActive(false);
        helmet01.SetActive(false);
        viking01.SetActive(false);
        king02.SetActive(false);
        leaf02.SetActive(false);
        sprout02.SetActive(false);

        GameState.Character current = GameState.GetCharacter();

        switch (current)
        {
            case GameState.Character.King_01:
                king01.SetActive(true);
                break;
            case GameState.Character.Helmet_01:
                helmet01.SetActive(true);
                break;
            case GameState.Character.Viking_01:
                viking01.SetActive(true);
                break;
            case GameState.Character.King_02:
                king02.SetActive(true);
                break;
            case GameState.Character.Leaf_02:
                leaf02.SetActive(true);
                break;
            case GameState.Character.Sprout_02:
                sprout02.SetActive(true);
                break;
            default:
                king01.SetActive(true);
                break;
        }

    }
}
