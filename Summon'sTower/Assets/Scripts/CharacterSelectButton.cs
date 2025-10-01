using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton: MonoBehaviour
{
    public CharacterData character; // ���̃{�^���̃L����
    public Image icon;
    public TextMeshProUGUI costText;

    private Button button;
    private PartyManager partyManager;

    

    void Awake()
    {
        // Button�R���|�[�l���g
        button = GetComponent<Button>();

        if (icon == null) icon = GetComponentInChildren<Image>();

        if (costText == null) costText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
       

        partyManager = FindFirstObjectByType<PartyManager>();
        icon.sprite = character.icon; // �����ڔ��f
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
            icon.color = new Color(1f, 1f, 1f, 0.4f); // ������
            if (button != null) button.interactable = false;
        }
    }

    public void OnClick()
    {
        Debug.Log("�����Ă�");

        if (character != null && character.isOwned)
        {
            Debug.Log("AssignCharacterToFirstEmptySlot");
            partyManager.AssignCharacterToFirstEmptySlot(character);
        }
    }
}
