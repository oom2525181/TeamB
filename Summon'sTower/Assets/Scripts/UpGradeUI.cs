using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [Header("表示用UI")]
    public Image selectedCharacterImage;
    public TextMeshProUGUI selectedCharacterNameText;
    public TextMeshProUGUI upgradeLevelText;

    public TextMeshProUGUI coinText;      //所持コイン
    public TextMeshProUGUI CountText; //キャラの所持数

    public Button upgradeButton;

    private CharacterData currentCharacter;

    void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButton);
        UpdateCoinDisplay();
    }

    // CharacterSelect2 から呼ばれる
    public void ShowCharacterInfo(CharacterData data)
    {
        currentCharacter = data;

        // UI更新
        selectedCharacterImage.sprite = data.icon;
        selectedCharacterNameText.text = data.characterName;
        upgradeLevelText.text = $"Lv. {data.UpGradeCount}";

        // コイン＆重複数更新
        UpdateCoinDisplay();
        UpdateCountDisplay();
    }

    // +ボタン押下時
    void OnUpgradeButton()
    {
        if (currentCharacter == null) return;

        int cost = 100; // コイン消費量（仮）
        int currentCoin = PlayerData.GetCoin();


        if (currentCharacter.collectCount > 1)
        {
            // 重複キャラを素材に強化
            currentCharacter.collectCount--;
            PlayerData.SaveCollectCount(currentCharacter.characterName, currentCharacter.collectCount);
            currentCharacter.UpGradeCount++;

            Debug.Log($"{currentCharacter.characterName} をアップグレード！（重複キャラ使用）");
        }
        else if (currentCoin >= cost)
        {
            // 通常のコイン消費による強化
            PlayerData.SaveCoin(currentCoin - cost);
            currentCharacter.UpGradeCount++;

            Debug.Log($"{currentCharacter.characterName} をアップグレード！（コイン消費）");
        }
        
        else
        {
            Debug.Log("コストも素材も足りません！");
            return;
        }

        PlayerData.SaveUpgradeCount(currentCharacter.characterName, currentCharacter.UpGradeCount);

        // UI更新
        upgradeLevelText.text = $"Lv. {currentCharacter.UpGradeCount}";
        UpdateCoinDisplay();
        UpdateCountDisplay();
    }

    void UpdateCoinDisplay()
    {
        int coin = PlayerData.GetCoin();
        if (coinText != null)
            coinText.text = $"Coins : {coin}";
    }

    void UpdateCountDisplay()
    {
        if (currentCharacter == null || CountText == null) return;
        int count = PlayerData.LoadCollectCount(currentCharacter.characterName);
        CountText.text = $"Owned : {count -1}";
    }
}