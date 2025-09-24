using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    //private float speed = 5.0f;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float hp;
    [SerializeField] float damage;

    public float damageInterval = 1f;
    private float lastDamageTime = 0f;

    public EnemySpawner spawner;
    
    private void Start()
    {
        if (speed <= 0) //デフォルトのスピード
            speed = 1.5f;
        if (hp <= 0) //デフォルトの体力
            hp = 5;
        if (CompareTag("Ally"))
        target = GameObject.Find("Enemy'sTower").transform;
        else if (CompareTag("Enemy"))
            target = GameObject.Find("Ally'sTower").transform;
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("ぶつかった");
            if (other.gameObject.CompareTag("Enemy") && CompareTag("Ally"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Ally") && CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            /*
            if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                Debug.Log("敗北");
                spawner.isEND = true;
            }
            else  if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
            {
               Destroy(other.gameObject);
               Destroy(gameObject);
               Debug.Log("勝利");
               spawner.isEND = true;
            }*/
    }
    public void OnCollisionStay2D(Collision2D other)
    {
        //Debug.Log("Stayよびだし");
        if (other.gameObject.CompareTag("Enemy'sTower") && CompareTag("Ally"))
        {
            //Debug.Log("Allyによるよびだし");
            Tower tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    tower.TakeDamage(damage, 1);
                    lastDamageTime = Time.time;
                }
            }
            else
            {
                Debug.Log("nulL!!!!!!");
            }
        }
        else if (other.gameObject.CompareTag("Ally'sTower") && CompareTag("Enemy"))
        {
            //Debug.Log("Enemyによるよびだし");
            Tower tower = other.gameObject.GetComponent<Tower>();
            if (tower != null)
            {
                if (Time.time - lastDamageTime >= damageInterval)
                {
                    tower.TakeDamage(damage, 2);
                    lastDamageTime = Time.time;
                }
            }
            else
            {
                Debug.Log("nulL!!!!!!");
            }
        }
    }
    void Update()
    {
        if (spawner.isSTOPED == false && spawner.isEND == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

}
