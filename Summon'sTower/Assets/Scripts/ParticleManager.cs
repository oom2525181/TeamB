using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [System.Serializable]
    public class ParticlePool
    {
        public string effectName;        // 識別用の名前
        public GameObject prefab;        // エフェクトのPrefab
        public int poolSize = 10;        // プールサイズ
    }

    [SerializeField] private List<ParticlePool> particlePools; // 複数のパーティクル設定リスト
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    public static ParticleManager Instance { get; private set; }

    private void Awake()
    {
        // 各パーティクルごとにプールを作成
        foreach (var pool in particlePools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            pools[pool.effectName] = queue;
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void PlayEffect(string effectName, Vector3 position, float lifeTime = 1f)
    {
        if (!pools.ContainsKey(effectName))
        {
            Debug.LogWarning($"Effect {effectName} は登録されていません");
            return;
        }

        var queue = pools[effectName];
        if (queue.Count > 0)
        {
            GameObject effect = queue.Dequeue();
            effect.transform.position = position;
            effect.SetActive(true);

            StartCoroutine(ReturnToPool(effectName, effect, lifeTime));
        }
    }

    private IEnumerator ReturnToPool(string effectName, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        pools[effectName].Enqueue(obj);
    }
}