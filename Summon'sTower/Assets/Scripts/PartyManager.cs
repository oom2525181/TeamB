using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    public CharacterData[] selectedParty = new CharacterData[5];
    private PartySlot[] slots;

    private CharacterData[] allCharacters;
    public CharacterData[] AllCharacters => allCharacters; // ������Q�Ɨp

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Resources/Characters �t�H���_����S�����[�h
            allCharacters = Resources.LoadAll<CharacterData>("Characters");

            // ����������
            foreach (var c in allCharacters)
            {
                c.isOwned = c.DefaultCharacter;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[���� PartySlots ������Ύ擾
        GameObject slotsParent = GameObject.Find("PartySlots");
        if (slotsParent != null)
        {
            slots = slotsParent.GetComponentsInChildren<PartySlot>();
            RefreshSlotsUI();
        }
    }

    // UI �ɔ��f
    public void RefreshSlotsUI()
    {
        if (slots == null) return;
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < selectedParty.Length)
                slots[i].SetCharacter(selectedParty[i]);
            else
                slots[i].SetCharacter(null);
        }
    }


    public void AssignCharacterToFirstEmptySlot(CharacterData c)
    {
        // ���łɑI������Ă����牽�����Ȃ�
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

        Debug.Log("�󂫘g������܂���I");
    }

    public void RemoveCharacter(CharacterData c)
    {
        for (int i = 0; i < selectedParty.Length; i++)
        {
            if (selectedParty[i] == c)
            {
                selectedParty[i] = null;
                if (slots != null && i < slots.Length)
                    slots[i].SetCharacter(null); // UI���X�V
                return;
            }
        }
    }
}