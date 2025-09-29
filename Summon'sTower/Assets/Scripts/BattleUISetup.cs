using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUISetup : MonoBehaviour
{
    public ButtonManager[] battleButtons; // �o�g���V�[���ɂ���{�^�����Ƀh���b�O

    void Start()
    {
        var party = PartyManager.Instance.selectedParty;

        for (int i = 0; i < battleButtons.Length && i < party.Length; i++)
        {
            battleButtons[i].SetCharacter(party[i]);
        }
    }
}