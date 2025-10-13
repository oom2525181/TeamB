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
    public int GetCoin = 0;

    public Dictionary<string, int> collectedCharacters = new Dictionary<string, int>();

    void Start()
    {
        this.moneytext = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetMoney", 0.09f, 0.09f);
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
    public void ShowResultInfo()
    {
        resultInfoPanel.SetActive(true);  // 結果パネルを表示  

        Dictionary<string, int> finalRewards = new Dictionary<string, int>();

          string result = "◆ 獲得したキャラクター\n";
        foreach (var pair in collectedCharacters)
        {
            result += $"{pair.Key} × {pair.Value}\n";
        }

        result += $"\n◆ 獲得コイン：{GetCoin}";

        resultInfoText.text = result;
    
        PlayerData.AddCoin(GetCoin);
    }
}
