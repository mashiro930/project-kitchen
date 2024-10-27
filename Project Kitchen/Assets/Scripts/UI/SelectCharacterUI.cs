using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private int slimeIndex = 0;

    public void OnPointerDownAction()
    {
        switch (slimeIndex)
        {
            case 0:
                GameState.SetCharacter(GameState.Character.King_01);
                break;
            case 1:
                GameState.SetCharacter(GameState.Character.Helmet_01);
                break;
            case 2:
                GameState.SetCharacter(GameState.Character.Viking_01);
                break;
            case 3:
                GameState.SetCharacter(GameState.Character.King_02);
                break;
            case 4:
                GameState.SetCharacter(GameState.Character.Leaf_02);
                break;
            case 5:
                GameState.SetCharacter(GameState.Character.Sprout_02);
                break;
            default:
                GameState.SetCharacter(GameState.Character.King_01);
                break;
        }
    }
}
