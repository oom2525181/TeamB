using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> enemiesInRange = new List<GameObject>();
    private GoTower parent;

    private void Start()
    {
        parent = GetComponentInParent<GoTower>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{parent.name} �� {other.name} �ƏՓ�");

        // �������猩�ēG���j�b�g or �G�̓��Ȃ�ǉ�
        if (parent.IsEnemy(other.gameObject) ||
            (parent.CompareTag("Ally") && other.CompareTag("Enemy'sTower")) ||
            (parent.CompareTag("Enemy") && other.CompareTag("Ally'sTower")))
        {
            if (!enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Add(other.gameObject);
                //Debug.Log($"{parent.name} �� {other.name} ���U���͈͂ɒǉ�");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}