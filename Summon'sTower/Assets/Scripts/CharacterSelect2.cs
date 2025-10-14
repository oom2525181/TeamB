using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelect2 : MonoBehaviour
{
    public CharacterData characterData;
    public Image characterIcon;
    //public TextMeshProUGUI characterNameText;
    private Button button;
    private UpgradeUI upgradeUI;

    void Start()
    {
        button = GetComponent<Button>();

        upgradeUI = FindFirstObjectByType<UpgradeUI>();
        characterIcon.sprite = characterData.icon;
        // characterNameText.text = characterData.characterName;
        UpdateOwnershipState();
    }

    public void OnSelectCharacter()
    {
        if (upgradeUI != null)
        {
            upgradeUI.ShowCharacterInfo(characterData);
        }
    }

    void UpdateOwnershipState()
    {
        if (characterData == null) return;

        if (characterData.isOwned)
        {
            characterIcon.color = Color.white;
            if (button != null) button.interactable = true;
        }
        else
        {
            characterIcon.color = new Color(1f, 1f, 1f, 0.4f); // ”¼“§–¾
            if (button != null) button.interactable = false;
        }
    }
}
