using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        var party = PartyManager.Instance.selectedParty; // PartyManagerから取得

        // 1人でもセットされていればOK
        bool hasCharacter = false;
        foreach (var c in party)
        {
            if (c != null)
            {
                hasCharacter = true;
                break;
            }
        }

        if (hasCharacter)
        {
            SceneManager.LoadScene("Main Scene");
        }
        else
        {
            Debug.Log("最低1人キャラをセットしてください！");
        }
    }
}