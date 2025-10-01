using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // このボタンのキャラ
    public Image icon;
    public TextMeshProUGUI costText;

    private Button button;
    private PartyManager partyManager;

    

    void Awake()
    {
        // Buttonコンポーネント
        button = GetComponent<Button>();

        if (icon == null) icon = GetComponentInChildren<Image>();

        if (costText == null) costText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
       

        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // 見た目反映
        //costText = GetComponentInChildren<TextMeshProUGUI>();
        if (character != null)
        {
            if (icon != null) icon.sprite = character.icon;
            if (costText != null) costText.text = $"{character.cost}";
        }
        UpdateOwnershipState();
    }

   

    void UpdateOwnershipState()
    {
        if (character == null) return;

        if (character.isOwned)
        {
            icon.color = Color.white;
            if (button != null) button.interactable = true;
        }
        else
        {
            icon.color = new Color(1f, 1f, 1f, 0.4f); // 半透明
            if (button != null) button.interactable = false;
        }
    }

    public void OnClick()
    {
        Debug.Log("押せてる");

        if (character != null && character.isOwned)
        {
            Debug.Log("AssignCharacterToFirstEmptySlot");
            partyManager.AssignCharacterToFirstEmptySlot(character);
        }
    }
}
