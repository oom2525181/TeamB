using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;  // �V���O���g��
    public List<CharacterData> selectedCharacters = new List<CharacterData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����ׂ��ł��j������Ȃ�
        }
        else
        {
            Destroy(gameObject); // 2�ڂ��o�Ȃ��悤��
        }
    }
}