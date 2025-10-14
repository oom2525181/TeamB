using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gacha1 : MonoBehaviour
{
    public CharacterData[] Lineup;

    [SerializeField] private Image resultIcon;
    [SerializeField] private TMPro.TMP_Text resultName;

    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pullボタンのクリック
    public void OnClick()
    {
        int currentCoin = PlayerData.GetCoin();
        if (currentCoin >= 100)
        {
            PlayerData.SaveCoin(currentCoin - 100);
            UpdateCoinDisplay();
            CharacterData result = PullGacha();
            ShowResult(result);
        }
        else
        {
            Debug.Log("お金が足りないよ!!!");
        }
    }

    //ガチャを引く
    public CharacterData PullGacha()
    {

        // 0〜Lineupの範囲でランダムに選ぶ
        int index = Random.Range(0, Lineup.Length);

        CharacterData result = Lineup[index];
        Debug.Log("当たったキャラ: " + result.name);

        int currentCount = PlayerData.LoadCollectCount(result.characterName);

        if (!PlayerData.IsCharacterOwned(result.characterName))
        {
            PlayerData.SaveCharacterOwned(result.characterName);
            currentCount = 1;
            result.isOwned = true;
        }
        else
        {
            // すでに所持している場合 → 重複入手
            currentCount++;
            Debug.Log($"{result.characterName} が重複 所持数: {currentCount}");
        }
        PlayerData.SaveCharacterOwned(result.characterName);
        result.collectCount = currentCount;
        PlayerData.SaveCollectCount(result.characterName, currentCount);

        return result;
    }

    //結果の表示
    public void ShowResult(CharacterData character)
    {
        resultIcon.sprite = character.icon;
        resultName.text = character.name;
    }

    void UpdateCoinDisplay()
    {
        int coin = PlayerData.GetCoin();
        if (coinText != null)
            coinText.text = $"Coins : {coin}";
    }
}
