using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSaveManager
{
    public static bool GetHaveStageStar(string stageName ,int num)
    {
        int temp = PlayerPrefs.GetInt(stageName + num,0);

        if(temp == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SetHaveStageStar(string stageName,int num,bool haveStar)
    {
        PlayerPrefs.SetInt(stageName + num, (haveStar) ? 1 : 0);
        PlayerPrefs.Save();
    }
    
}
