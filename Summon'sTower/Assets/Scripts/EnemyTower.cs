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
        Debug.Log("�G�̃^���[��"+ damage + " �_���[�W��^����HP�� " + TowerHP + " �ɂȂ����I");
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
