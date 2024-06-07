using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSystem 
{
    const string MONEY = "MONEY";
    public static void SaveMoney(int num)
    {
        PlayerPrefs.SetInt(MONEY, num);
    }
    public static void LoadMoney(int num)
    {
        PlayerPrefs.GetInt(MONEY, 0);
    }

    public static void SaveIntData(string name, int val)
    {
        PlayerPrefs.SetInt(name,val);
    }
    public static int GetIntData(string name)
    {
        return PlayerPrefs.GetInt(name,0);
    }
}
