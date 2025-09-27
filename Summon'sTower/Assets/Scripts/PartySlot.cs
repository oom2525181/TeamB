using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartySlot: MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI costText;
    public CharacterData assignedCharacter;

    public void SetCharacter(CharacterData character)
    {
        assignedCharacter = character;

        if (character != null)
        {
            icon.sprite = character.icon;
            //costText.text = $"Cost: {character.cost}";
            costText.text = $"{character.cost}";
        }
        else
        {
            icon.sprite = null;
            costText.text = "";
        }
    }

    public void OnClick()
    {
        if (assignedCharacter != null)
        {
            // PartyManager Ç©ÇÁçÌèú
            PartyManager.Instance.RemoveCharacter(assignedCharacter);
        }

        // UIÇÉNÉäÉA
        SetCharacter(null);
    }
}