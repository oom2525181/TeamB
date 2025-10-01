using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public static void SaveCharacterOwned(string characterName)
    {
        PlayerPrefs.SetInt(characterName + "_Owned", 1);
        PlayerPrefs.Save();
    }

    public static bool IsCharacterOwned(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + "_Owned", 0) == 1;
    }
}