using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 6.5f;
    public int Count;
    public bool isSTOPED = false;
    public bool isEND = false;
    public GameObject STOPPanel;
    public GameObject ENDPanel;

    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;
    [SerializeField] GameObject Enemy4;
    [SerializeField] GameObject Enemy5;
    [SerializeField] GameObject Enemy6;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Ally'sTower").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isEND == false)
        {
            STOP();
        }
        if (isEND == true)
        {
            STOPPanel.SetActive(false);
            ENDPanel.SetActive(true);
        }

        timer += Time.deltaTime;
        if(timer >= interval)
        {
            if (isSTOPED == false && isEND == false)
            {
                EnemySpawn();
            }
            timer = 0f;
        }
    }
    void EnemySpawn()
    {
        int TypeEnemy = Random.Range(1, 6);
        if (TypeEnemy == 1)
        {
            int EnemyCount = Random.Range(1, 7);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy1, new Vector3(5.7f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 2)
        {
            int EnemyCount = Random.Range(1, 7);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy2, new Vector3(5.7f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 3)
        {
            int EnemyCount = Random.Range(1, 7);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy3, new Vector3(5.7f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 4)
        {
            int EnemyCount = Random.Range(1, 7);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy4, new Vector3(5.7f, y, 0), transform.rotation);
            }
        }
        else if (TypeEnemy == 5)
        {
            int EnemyCount = Random.Range(1, 7);
            for (int i = 0; i < EnemyCount; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(Enemy5, new Vector3(5.7f, y, 0), transform.rotation);
            }
        }
    }
    public void STOP()
    {
        if (isSTOPED == true)
        {
            isSTOPED = false;
            STOPPanel.SetActive(false);
        }
        else
        {
            isSTOPED = true;
            STOPPanel.SetActive(true);
        }
    }
}
