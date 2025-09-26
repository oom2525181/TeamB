using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 3.5f;
    public int Count;
    public bool isSTOPED = false;
    public bool isEND = false;
    public GameObject STOPPanel;
    public GameObject ENDPanel;

    //[SerializeField] GameObject Enemy1;
    //[SerializeField] GameObject Enemy2;
    //[SerializeField] GameObject Enemy3;
    //[SerializeField] GameObject Enemy4;
    //[SerializeField] GameObject Enemy5;
    //[SerializeField] GameObject Enemy6;
    [SerializeField] private GameObject[] enemies;

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
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];
        int enemyCount = Random.Range(1, 5);
        if (enemyPrefab == enemies[5])
            enemyCount = 2;
        else if (enemyPrefab == enemies[6])
            enemyCount = 1;


        for (int i = 0; i < enemyCount; i++)
        {
            float y = Random.Range(-1.0f, 0.4f);
            Instantiate(enemyPrefab, new Vector3(7.35f, y, 0), Quaternion.identity);
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
