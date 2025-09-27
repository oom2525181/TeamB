using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // このボタンが表すキャラ
    public Image icon;
    private PartyManager partyManager;

    void Start()
    {
        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // 見た目反映
    }

    public void OnClick()
    {
        // 空いてる枠にキャラを入れる
        partyManager.AssignCharacterToFirstEmptySlot(character);
    }
}
