using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaveData
{
    private const string str_name = "CharacterName10";
    public static void SetName(string value)
    {
        PlayerPrefs.SetString(str_name, value);
        PlayerPrefs.Save();
    }
    public static string GetName()
    {
        return "minf";
        //		return PlayerPrefs.GetString (str_name, "minf").Trim();
    }
    public static void DataSave(string value)
    {
        PlayerPrefs.SetString("JobClassName", value);
        PlayerPrefs.Save();
    }

    public static string DataLoad()
    {
        return PlayerPrefs.GetString("JobClassName", "");
    }
}

