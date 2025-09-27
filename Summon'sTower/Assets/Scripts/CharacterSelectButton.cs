using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // ���̃{�^�����\���L����
    public Image icon;
    public TextMeshProUGUI costText;

    private PartyManager partyManager;

    void Start()
    {
        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // �����ڔ��f
        costText = GetComponentInChildren<TextMeshProUGUI>();
        if (character != null)
        {
            if (icon != null) icon.sprite = character.icon;
            if (costText != null) costText.text = $"{character.cost}";
        }
    }

    public void OnClick()
    {
        // �󂢂Ă�g�ɃL����������
        partyManager.AssignCharacterToFirstEmptySlot(character);
    }
}
