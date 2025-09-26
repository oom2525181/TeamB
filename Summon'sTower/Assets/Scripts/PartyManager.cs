using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public ButtonManager[] partyButtons;      // Inspector�Ń{�^�����Z�b�g
    public CharacterData[] selectedParty;     // �Ґ������L�����z��

    private void Start()
    {
        SetParty(); // Start�ŌĂ�
    }

    // �Ґ���ʂŌĂԏ���
    public void SetParty()
    {
        for (int i = 0; i < selectedParty.Length && i < partyButtons.Length; i++)
        {
            partyButtons[i].SetCharacter(selectedParty[i]);
        }
    }
}
