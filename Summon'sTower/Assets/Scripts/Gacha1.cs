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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //pullボタンのクリック
    public void OnClick()
    {
        CharacterData result = PullGacha();
        ShowResult(result);
    }

    //ガチャを引く
    public CharacterData PullGacha()
    {
        // 0〜Lineupの範囲でランダムに選ぶ
        int index = Random.Range(0, Lineup.Length);

        CharacterData result = Lineup[index];
        Debug.Log("当たったキャラ: " + result.name);
        result.isOwned = true;
        PlayerData.SaveCharacterOwned(result.characterName);

        return result;
    }

    //結果の表示
    public void ShowResult(CharacterData character)
    {
        resultIcon.sprite = character.icon;
        resultName.text = character.name;
    }
}
