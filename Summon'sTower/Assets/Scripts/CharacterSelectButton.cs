using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // このボタンが表すキャラ
    public Image icon;
    public TextMeshProUGUI costText;

    private PartyManager partyManager;

    void Start()
    {
        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // 見た目反映
        costText = GetComponentInChildren<TextMeshProUGUI>();
        if (character != null)
        {
            if (icon != null) icon.sprite = character.icon;
            if (costText != null) costText.text = $"{character.cost}";
        }
    }

    public void OnClick()
    {
        // 空いてる枠にキャラを入れる
        partyManager.AssignCharacterToFirstEmptySlot(character);
    }
}
