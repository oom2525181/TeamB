using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyTower : MonoBehaviour
{
    public float TowerHP = 500.0f;
    public bool broken = false;
    public void Damage(float damage)
    {
        TowerHP -= damage;
        Debug.Log("味方のタワーが"+ damage + " ダメージを受けてHPが " + TowerHP + " になった！");
    }
    void Update()
    {
        if (TowerHP <= 0)
        {
            broken = true;
            Destroy(gameObject);
        }
    }
}
