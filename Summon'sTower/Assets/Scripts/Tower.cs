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

    private ParticleManager particleManager; //�p�[�e�B�N���p

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
            panel.SetActive(false); // ��\��
        }

        particleManager = FindFirstObjectByType<ParticleManager>();
    }
    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        Debug.Log($"{name} �� {dmg} �_���[�W���󂯂��I �c��HP: {hp}");

        if (particleManager != null)
            particleManager.PlayEffect(transform.position);

        //if(type== 1)
        //    Debug.Log("�G�̃^���[��" + dmg + " �_���[�W��^����HP�� " + hp + " �ɂȂ����I");
        //else
        //    Debug.Log("�����̃^���[��" + dmg + " �_���[�W���󂯂�HP�� " + hp + " �ɂȂ����I");

        hp = Mathf.Clamp(hp, 0, MaxHP);

        if (hpGaugeImage != null) 
        {
            hpGaugeImage.fillAmount = hp / MaxHP;
        }

        //Debug.Log("TowerHP =" + HP);
        if(hp <= 0)
        {
            Debug.Log($"{name} ���j�󂳂ꂽ�I");
            if (panel != null)
            {
                panel.SetActive(true); // �p�l���\��
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


