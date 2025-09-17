using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    private float speed = 5.0f;
    private Transform target;
    void Start()
    {
        if (CompareTag("Ally"))
            target = GameObject.Find("Enemy'sTower").transform;
        else if (CompareTag("Enemy"))
            target = GameObject.Find("Ally'sTower").transform;
    }
    void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (CompareTag("Ally"))
        {
            if (collision.gameObject.GetComponent<EnemyTower>())
            {
                collision.gameObject.GetComponent<EnemyTower>().Damage(10);
            }
        }

        else if (CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<AllyTower>())
            {
                collision.gameObject.GetComponent<AllyTower>().Damage(10);
            }
        }
    }
}