using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public float MaxHP = 500.0f;
    float HP;
    public EnemySpawner spawner;
    GameObject HpGauge;

    [SerializeField] private Image hpGaugeImage;

    private void Start()
    {
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this.HpGauge = GameObject.Find("EnemyHpGauge");
        HP = MaxHP;
        if(hpGaugeImage != null )
        {
            hpGaugeImage.fillAmount = 1f;
        }
    }
    public void TakeDamage(float damage,int type)
    {
        HP -= damage;
        if(type== 1)
            Debug.Log("敵のタワーに" + damage + " ダメージを与えてHPが " + HP + " になった！");
        else
            Debug.Log("味方のタワーが" + damage + " ダメージを受けてHPが " + HP + " になった！");

        HP = Mathf.Clamp(HP, 0, MaxHP);

        if (hpGaugeImage != null) 
        {
            hpGaugeImage.fillAmount = HP / MaxHP;
        }

        //Debug.Log("TowerHP =" + HP);
        if(HP <= 0)
        {
            SceneManager.LoadScene("EndScene");
            //Destroy(gameObject);
            //spawner.isEND = true;
           
        }
    }
    public void ChangeHP()
    {
        float fillAmount = HP / MaxHP;
    }
}


