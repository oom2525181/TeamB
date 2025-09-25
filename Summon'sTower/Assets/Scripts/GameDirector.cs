using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    TextMeshProUGUI moneytext;
    public int money;
    public EnemySpawner spawner;

    void Start()
    {
        this.moneytext = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetMoney", 0.1f, 0.1f);
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = $"{money:F0}";
        //money++;
    }
    void GetMoney()
    {
       // Debug.Log("÷Àﬁ¿ﬁª⁄¿÷");
       if(spawner.isSTOPED == false)
        money++;
        //money += 10;
    }
   
}
