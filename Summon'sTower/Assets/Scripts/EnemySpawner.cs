using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 2f;
    public int Count;

    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;
    [SerializeField] GameObject Enemy4;
    [SerializeField] GameObject Enemy5;
    [SerializeField] GameObject Enemy6;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            EnemySpawn();
            timer = 0f;
        }
    }
    void EnemySpawn()
    {
        int TypeEnemy = Random.Range(1, 6);
        if (TypeEnemy == 1)
        {
            int EnemyCount = Random.Range(1, 4);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy1, new Vector3(5.4f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 2)
        {
            int EnemyCount = Random.Range(1, 4);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy2, new Vector3(5.4f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 3)
        {
            int EnemyCount = Random.Range(1, 4);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy3, new Vector3(5.4f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 4)
        {
            int EnemyCount = Random.Range(1, 4);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy4, new Vector3(5.4f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 5)
        {
            int EnemyCount = Random.Range(1, 4);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy5, new Vector3(5.4f, y, 0), transform.rotation);
            }
        }
    }
}
