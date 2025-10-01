using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public float MaxHP = 500.0f;
    public float hp;
    public EnemySpawner spawner;
    GameObject HpGauge;

    [SerializeField] private Image hpGaugeImage;

    [SerializeField] private GameObject panel;

    private ParticleManager particleManager; //パーティクル用

    private void Start()
    {
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this.HpGauge = GameObject.Find("EnemyHpGauge");
        hp = MaxHP;
        if(hpGaugeImage != null )
        {
            hpGaugeImage.fillAmount = 1f;
        }

        if (panel != null)
        {
            panel.SetActive(false); // 非表示
        }

        particleManager = FindFirstObjectByType<ParticleManager>();
    }
    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        Debug.Log($"{name} が {dmg} ダメージを受けた！ 残りHP: {hp}");

        if (particleManager != null)
            particleManager.PlayEffect(transform.position);

        //if(type== 1)
        //    Debug.Log("敵のタワーに" + dmg + " ダメージを与えてHPが " + hp + " になった！");
        //else
        //    Debug.Log("味方のタワーが" + dmg + " ダメージを受けてHPが " + hp + " になった！");

        hp = Mathf.Clamp(hp, 0, MaxHP);

        if (hpGaugeImage != null) 
        {
            hpGaugeImage.fillAmount = hp / MaxHP;
        }

        //Debug.Log("TowerHP =" + HP);
        if(hp <= 0)
        {
            Debug.Log($"{name} が破壊された！");
            if (panel != null)
            {
                panel.SetActive(true); // パネル表示
            }
            Destroy(gameObject);
            //SceneManager.LoadScene("EndScene");
            //Destroy(gameObject);
            //spawner.isEND = true;
           
        }
    }
    public void ChangeHP()
    {
        float fillAmount = hp / MaxHP;
    }
}


