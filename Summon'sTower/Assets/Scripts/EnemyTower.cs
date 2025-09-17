using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour
{
    public float TowerHP = 500.0f;
    public bool broken = false;
    public void Damage(float damage)
    {
        TowerHP -= damage;
        Debug.Log("敵のタワーに"+ damage + " ダメージを与えてHPが " + TowerHP + " になった！");
    }
    void Update()
    {
        if (TowerHP <= 0)
        {
            broken= true;
            Destroy(gameObject);
        }
    }
} 
