using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    public CharacterData[] selectedParty = new CharacterData[5];
    private PartySlot[] slots;

    public CharacterData[] allCharacters;
    public CharacterData[] AllCharacters => allCharacters; // ������Q�Ɨp

    void Awake()
    {
        //foreach (var c in allCharacters)
        //{
        //    c.isOwned = false; // ������������Z�b�g
        //    PlayerPrefs.DeleteKey(c.characterName + "_Owned"); // �ۑ�������
        //}

        //PlayerPrefs.DeleteAll();

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
                if (c.isOwned) // �������Ă���ꍇ�����ۑ�
                {
                    PlayerData.SaveCharacterOwned(c.characterName);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        foreach (var c in allCharacters)
        {
            c.isOwned = PlayerData.IsCharacterOwned(c.characterName);
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
    public CharacterData GetCharacterByName(string name)
    {
        foreach (var c in allCharacters)
        {
            if (c.characterName == name)
                return c;
        }
        Debug.LogWarning($"[PartyManager] �L�����N�^�[ '{name}' ��������܂���B");
        return null;
    }
}