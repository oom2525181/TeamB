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
        Debug.Log("�����̃^���[��"+ damage + " �_���[�W���󂯂�HP�� " + TowerHP + " �ɂȂ����I");
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
