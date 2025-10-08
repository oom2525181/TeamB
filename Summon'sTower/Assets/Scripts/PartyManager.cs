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
    public CharacterData[] AllCharacters => allCharacters; // 他から参照用

    void Awake()
    {
        //foreach (var c in allCharacters)
        //{
        //    c.isOwned = false; // メモリ上をリセット
        //    PlayerPrefs.DeleteKey(c.characterName + "_Owned"); // 保存も消す
        //}

        //PlayerPrefs.DeleteAll();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            

            // Resources/Characters フォルダから全部ロード
            allCharacters = Resources.LoadAll<CharacterData>("Characters");

            // 所持初期化
            foreach (var c in allCharacters)
            {
                c.isOwned = c.DefaultCharacter;
                if (c.isOwned) // 所持している場合だけ保存
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
        // シーンに PartySlots があれば取得
        GameObject slotsParent = GameObject.Find("PartySlots");
        if (slotsParent != null)
        {
            slots = slotsParent.GetComponentsInChildren<PartySlot>();
            RefreshSlotsUI();
        }
    }

    // UI に反映
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
    public CharacterData GetCharacterByName(string name)
    {
        foreach (var c in allCharacters)
        {
            if (c.characterName == name)
                return c;
        }
        Debug.LogWarning($"[PartyManager] キャラクター '{name}' が見つかりません。");
        return null;
    }
}