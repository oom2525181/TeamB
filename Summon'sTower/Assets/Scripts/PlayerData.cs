using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //キャラ所持を保存
    public static void SaveCharacterOwned(string characterName)
    {
        PlayerPrefs.SetInt(characterName + "_Owned", 1);
        PlayerPrefs.Save();
    }

    public static bool IsCharacterOwned(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + "_Owned", 0) == 1;
    }

    //ステージクリアを保存
    public static void SaveStageCleared(int stageNumber)
    {
        Debug.Log($"SaveStageCleared 呼び出し: stageNumber = {stageNumber}");
        int lastCleared = GetLastClearedStage();
        Debug.Log($"Save前: 最後にクリアしたステージ = {lastCleared}");
        if (stageNumber > lastCleared)
        {
            PlayerPrefs.SetInt("StageCleared", stageNumber);
            PlayerPrefs.Save();
            Debug.Log($"保存完了: StageCleared = {stageNumber}");
        }
    }

    public static int GetLastClearedStage()
    {
        return PlayerPrefs.GetInt("StageCleared", 0);
    }

    public static bool IsStageCleared(int stageNumber)
    {
        return GetLastClearedStage() >= stageNumber;
    }
}