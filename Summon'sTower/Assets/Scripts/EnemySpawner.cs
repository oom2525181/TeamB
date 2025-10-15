using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


//public class EnemySpawner : MonoBehaviour
//{
//    [System.Serializable]
//    public class Wave
//    {
//        public GameObject[] enemyPrefabs; // このWaveで出る敵
//        public int count = 1;             // 出現数
//        public float interval = 3.5f;     // 出現間隔
//    }
//    [SerializeField] private Wave[] waves;




//    private float timer = 0f;
//    private float interval = 3.5f;
//    public int Count;
//    public bool isSTOPED = false;
//    public bool isEND = false;
//    public GameObject STOPPanel;
//    public GameObject ENDPanel;

//    [SerializeField] private GameObject[] enemies;

//    Transform target;

//    // Start is called before the first frame update
//    void Start()
//    {
//        target = GameObject.Find("Ally'sTower").transform;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Escape) && isEND == false)
//        {
//            STOP();
//        }
//        if (isEND == true)
//        {
//            STOPPanel.SetActive(false);
//            ENDPanel.SetActive(true);
//        }

//        timer += Time.deltaTime;
//        if(timer >= interval)
//        {
//            if (isSTOPED == false && isEND == false)
//            {
//                EnemySpawn();
//            }
//            timer = 0f;
//        }
//    }
//    void EnemySpawn()
//    {
//        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];
//        int enemyCount = Random.Range(1, 5);
//        if (enemyPrefab == enemies[5])
//            enemyCount = 2;
//        else if (enemyPrefab == enemies[6])
//            enemyCount = 1;


//        for (int i = 0; i < enemyCount; i++)
//        {
//            float y = Random.Range(-1.0f, 0.4f);
//            Instantiate(enemyPrefab, new Vector3(7.35f, y, 0), Quaternion.identity);
//        }
//    }
//    public void STOP()
//    {
//        if (isSTOPED == true)
//        {
//            isSTOPED = false;
//            STOPPanel.SetActive(false);
//        }
//        else
//        {
//            isSTOPED = true;
//            STOPPanel.SetActive(true);
//        }
//    }
//}

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs; // このWaveで出る敵
    public int count = 1;             // 出現数
    public float interval = 3.5f;     // 出現間隔
    public bool isLoop = false;

    [HideInInspector] public float timer = 0f;
}

public class EnemySpawner: MonoBehaviour
{
        [SerializeField] private Wave[] waves;

        [SerializeField] private StageConfig stageConfig;
    private List<Wave> loopWaves = new List<Wave>();
    private int currentWaveIndex = 0;
    private float timer = 0f;

    GameObject director;
    public GameDirector gameDirector;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        gameDirector = director.GetComponent<GameDirector>();
    }

    void Update()
    {
        if (!gameDirector.isEND)
        {
            // 通常Waveの進行
            if (currentWaveIndex < stageConfig.waves.Length)
            {
                var wave = stageConfig.waves[currentWaveIndex];
                timer += Time.deltaTime;
                if (timer >= wave.interval)
                {
                    SpawnWave(wave);
                    timer = 0f;

                    if (wave.isLoop)
                    {
                        loopWaves.Add(wave); // ループ用リストに登録
                    }

                    currentWaveIndex++; // 次のWaveへ進む
                }
            }


            // ループWaveの処理
            foreach (var loop in loopWaves)
            {
                loop.timer += Time.deltaTime;
                if (loop.timer >= loop.interval)
                {
                    SpawnWave(loop);
                    loop.timer = 0f;
                }
            }
        }
    }

    void SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.count; i++)
        {
            float[] yPositions = { 2.2f, -1.2f, 2.5f };

            var prefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

            if (prefab == null)
            {
                Debug.LogError($"Wave {wave} の enemyPrefabs に null が含まれています。");
                continue;
            }

            if (prefab.scene.rootCount != 0)
            {
                Debug.LogError($"Wave {wave} の {prefab.name} はシーン上のオブジェクトを参照しています！Prefabを登録してください。");
                continue;
            }

            //float y = Random.Range(-0.88f, -0.48f);
            float y = yPositions[Random.Range(0, yPositions.Length)];
            Instantiate(prefab, new Vector3(7.35f, y, 0), Quaternion.identity);
        }
    }
    public void StageCleared()
    {
        //Debug.Log($"StageCleared 呼び出し: stageNumber = {stageConfig.stageNumber}");
        PlayerData.SaveStageCleared(stageConfig.stageNumber);
        stageConfig.isCleared = true;
    }
}
