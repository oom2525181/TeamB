using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab; // �p�[�e�B�N����Prefab
    [SerializeField] private int poolSize = 10;          // �v�[���̐�

    private Queue<GameObject> particlePool = new Queue<GameObject>();

    private void Awake()
    {
        // �v�[����������
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

            // ��莞�Ԍ�ɔ�A�N�e�B�u�����ăv�[���ɖ߂�
            StartCoroutine(ReturnToPool(effect, 1f)); // 1�b��ɖ߂�
        }
    }

    private IEnumerator<WaitForSeconds> ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        particlePool.Enqueue(obj);
    }
}