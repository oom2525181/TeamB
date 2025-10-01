using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab; // パーティクルのPrefab
    [SerializeField] private int poolSize = 10;          // プールの数

    private Queue<GameObject> particlePool = new Queue<GameObject>();

    private void Awake()
    {
        // プールを初期化
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(hitEffectPrefab);
            obj.SetActive(false);
            particlePool.Enqueue(obj);
        }
    }

    public void PlayEffect(Vector3 position)
    {
        if (particlePool.Count > 0)
        {
            GameObject effect = particlePool.Dequeue();
            effect.transform.position = position;
            effect.SetActive(true);

            // 一定時間後に非アクティブ化してプールに戻す
            StartCoroutine(ReturnToPool(effect, 1f)); // 1秒後に戻す
        }
    }

    private IEnumerator<WaitForSeconds> ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePool.Enqueue(obj);
    }
}