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
        //Debug.Log($"SaveStageCleared 呼び出し: stageNumber = {stageNumber}");
        int lastCleared = GetLastClearedStage();
        //Debug.Log($"Save前: 最後にクリアしたステージ = {lastCleared}");
        if (stageNumber > lastCleared)
        {
            PlayerPrefs.SetInt("StageCleared", stageNumber);
            PlayerPrefs.Save();
            //Debug.Log($"保存: StageCleared = {stageNumber}");
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

    // キャラの所持数
    public static void SaveCollectCount(string characterName, int count)
    {
        PlayerPrefs.SetInt(characterName + "_CollectCount", count);
        PlayerPrefs.Save();
    }

    public static int LoadCollectCount(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + "_CollectCount", 0);
    }
    //アップグレードした回数
    public static void SaveUpgradeCount(string characterName, int count)
    {
        PlayerPrefs.SetInt(characterName + "_UpgradeCount", count);
        PlayerPrefs.Save();
    }

    public static int LoadUpgradeCount(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + "_UpgradeCount", 0);
    }


    // コインを保存する
    public static void SaveCoin(int coin)
    {
        PlayerPrefs.SetInt("PlayerCoin", coin);
        PlayerPrefs.Save();
    }

    // コインを取得する
    public static int GetCoin()
    {
        return PlayerPrefs.GetInt("PlayerCoin", 0);
    }
    //持ってるコインに獲得したコインを追加
    public static void AddCoin(int amount)
    {
        int currentCoin = GetCoin();
        currentCoin += amount;
        SaveCoin(currentCoin);
    }
}