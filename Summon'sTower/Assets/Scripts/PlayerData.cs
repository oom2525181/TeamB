using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //�L����������ۑ�
    public static void SaveCharacterOwned(string characterName)
    {
        PlayerPrefs.SetInt(characterName + "_Owned", 1);
        PlayerPrefs.Save();
    }

    public static bool IsCharacterOwned(string characterName)
    {
        return PlayerPrefs.GetInt(characterName + "_Owned", 0) == 1;
    }

    //�X�e�[�W�N���A��ۑ�
    public static void SaveStageCleared(int stageNumber)
    {
        Debug.Log($"SaveStageCleared �Ăяo��: stageNumber = {stageNumber}");
        int lastCleared = GetLastClearedStage();
        Debug.Log($"Save�O: �Ō�ɃN���A�����X�e�[�W = {lastCleared}");
        if (stageNumber > lastCleared)
        {
            PlayerPrefs.SetInt("StageCleared", stageNumber);
            PlayerPrefs.Save();
            Debug.Log($"�ۑ�����: StageCleared = {stageNumber}");
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