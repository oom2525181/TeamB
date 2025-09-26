using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public ButtonManager[] partyButtons;      // Inspectorでボタンをセット
    public CharacterData[] selectedParty;     // 編成したキャラ配列

    private void Start()
    {
        SetParty(); // Startで呼ぶ
    }

    // 編成画面で呼ぶ処理
    public void SetParty()
    {
        for (int i = 0; i < selectedParty.Length && i < partyButtons.Length; i++)
        {
            partyButtons[i].SetCharacter(selectedParty[i]);
        }
    }
}
