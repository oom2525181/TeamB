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

    void Start()
    {
        this.moneytext = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetMoney", 0.1f, 0.1f);
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
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
            STOPPanel.SetActive(false);
            ENDPanel.SetActive(true);
        }

    }
    void GetMoney()
    {
       // Debug.Log("÷Àﬁ¿ﬁª⁄¿÷");
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

}
