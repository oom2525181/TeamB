using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;
    public CharacterData[] selectedParty = new CharacterData[5];
    PartySlot[] slots;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        slots = GameObject.Find("PartySlots")?.GetComponentsInChildren<PartySlot>();
        if (slots != null)
            for (int i = 0; i < slots.Length; i++)
                slots[i].SetCharacter(selectedParty[i]);
    }

    public void AssignCharacterToFirstEmptySlot(CharacterData c)
    {
        // すでに選択されていたら何もしない
        foreach (var selected in selectedParty)
            if (selected == c)
                return;

        for (int i = 0; i < selectedParty.Length; i++)
        {
            if (selectedParty[i] == null)
            {
                selectedParty[i] = c;
                if (slots != null && i < slots.Length)
                    slots[i].SetCharacter(c);
                return;
            }
        }

        Debug.Log("空き枠がありません！");
    }

    public void RemoveCharacter(CharacterData c)
    {
        for (int i = 0; i < selectedParty.Length; i++)
        {
            if (selectedParty[i] == c)
            {
                selectedParty[i] = null;
                if (slots != null && i < slots.Length)
                    slots[i].SetCharacter(null); // UIも更新
                return;
            }
        }
    }
}