using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [System.Serializable]
    public class ParticlePool
    {
        public string effectName;        // ���ʗp�̖��O
        public GameObject prefab;        // �G�t�F�N�g��Prefab
        public int poolSize = 10;        // �v�[���T�C�Y
    }

    [SerializeField] private List<ParticlePool> particlePools; // �����̃p�[�e�B�N���ݒ胊�X�g
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();

    public static ParticleManager Instance { get; private set; }

    private void Awake()
    {
        // �e�p�[�e�B�N�����ƂɃv�[�����쐬
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
            Debug.LogWarning($"Effect {effectName} �͓o�^����Ă��܂���");
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