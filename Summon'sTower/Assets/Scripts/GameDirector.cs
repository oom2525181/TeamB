using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    TextMeshProUGUI moneytext;

    public int money;
    public int moneymax = 100;
    public EnemySpawner spawner;

    public bool isSTOPED = false;
    public bool isEND = false;
    public GameObject STOPPanel;
    public GameObject ENDPanel;

    public TextMeshProUGUI resultInfoText;
    public GameObject resultInfoPanel;         //結果表示用
    private bool resultShown = false;
    public int GetCoin = 0;

    public Dictionary<string, int> collectedCharacters = new Dictionary<string, int>();

    void Start()
    {
        this.moneytext = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetMoney", 0.07f, 0.07f);
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();

        if (resultInfoText == null)
        {
            GameObject textObj = GameObject.Find("ResultInfoText");
                resultInfoText = textObj.GetComponent<TextMeshProUGUI>();
            if (resultInfoText == null)
                Debug.LogWarning("ResultInfoText      ?   ?   ?    I");
        }

        resultInfoPanel = GameObject.Find("ResultInfoPanel");
        if (resultInfoPanel != null)
            resultInfoPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = $"{money:F0} / {moneymax:F0}";
        //money++;

        if (Input.GetKeyDown(KeyCode.Escape) && isEND == false)
        {
            STOP();
        }
        if (isEND == true)
        {
            ShowResultInfo();
            STOPPanel.SetActive(false);
            //ENDPanel.SetActive(true);
        }

    }
    void GetMoney()
    {
       // Debug.Log("ﾖﾋﾞﾀﾞｻﾚﾀﾖ");
       if(isSTOPED == false && money < moneymax)
        money++;
        //money += 10;
    }

    public void STOP()
    {
        if (isSTOPED == true)
        {
            isSTOPED = false;
            STOPPanel.SetActive(false);
        }
        else
        {
            isSTOPED = true;
            STOPPanel.SetActive(true);
        }
    }

    
       public void AddCharacter(string characterName)
    {
        if (collectedCharacters.ContainsKey(characterName))
        {
            collectedCharacters[characterName]++;
        }
        else
        {
            collectedCharacters[characterName] = 1;
        }
    }


    public void AddCoin(int num)
    {
        GetCoin += num;
        Debug.Log($"コイン獲得: +{num}（合計 {GetCoin}）");
    }


    public void ShowResultInfo()
    {
        if (resultShown) return;  // すでに表示済みなら何もしない
        resultShown = true;

        resultInfoPanel.SetActive(true);

        string result = " Dropped Characters\n";

        foreach (var pair in collectedCharacters)
        {
            CharacterData rewardCharacter = PartyManager.Instance.GetCharacterByName(pair.Key);
            if (rewardCharacter != null)
            {
                // 所持数の保存
                int currentCount = PlayerData.LoadCollectCount(rewardCharacter.characterName);
                currentCount += pair.Value;
                rewardCharacter.collectCount = currentCount;

                // 保存
                PlayerData.SaveCollectCount(rewardCharacter.characterName, currentCount);


                if (!rewardCharacter.isOwned)
                {
                    rewardCharacter.isOwned = true;
                    PlayerData.SaveCharacterOwned(rewardCharacter.characterName);
                    Debug.Log($"{rewardCharacter.characterName} を初めて入手しました！");
                }
                else
                {
                    Debug.Log($"{rewardCharacter.characterName} を追加で入手！ (x{rewardCharacter.collectCount})");
                }

                result += $"{pair.Key} × {pair.Value}\n";
            }
        }

        PlayerPrefs.Save();

        result += $"\n    Dropped Coin : {GetCoin}";
        resultInfoText.text = result;

        Debug.Log(result);

        PlayerData.AddCoin(GetCoin);
        collectedCharacters.Clear();
    }
}
