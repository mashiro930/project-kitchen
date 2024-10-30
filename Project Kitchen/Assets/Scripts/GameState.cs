using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    public enum Character
    {
        King_01,
        Helmet_01,
        Viking_01,
        King_02,
        Leaf_02,
        Sprout_02
    }

    private static Character currentChar = Character.King_01;
    private static int starNumber_01 = 0;
    private static int starNumber_02 = 0;

    public static Character GetCharacter()
    {
        return currentChar;
    }

    public static void SetCharacter(Character character)
    {
        currentChar = character;
    }

    public static int GetStarNumber01()
    {
        return starNumber_01;
    }
    public static int GetStarNumber02()
    {
        return starNumber_02;
    }

    public static void SetStarNumber(int level, int star)
    {
        if (level == 1)
        {
            starNumber_01 = star;
        }
        else if(level == 2)
        {
            starNumber_02 = star;
        }
    }

    public static int GetStarNumber(int level)
    {
        if (level == 1)
        {
            return starNumber_01;
        }
        else if (level == 2)
        {
            return starNumber_02;
        }
        else
        {
            return 0;
        }
    }

}
