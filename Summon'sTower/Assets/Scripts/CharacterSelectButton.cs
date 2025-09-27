using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // ���̃{�^�����\���L����
    public Image icon;
    private PartyManager partyManager;

    void Start()
    {
        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // �����ڔ��f
    }

    public void OnClick()
    {
        // �󂢂Ă�g�ɃL����������
        partyManager.AssignCharacterToFirstEmptySlot(character);
    }
}
