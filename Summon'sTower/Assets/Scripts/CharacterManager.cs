using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;  // シングルトン
    public List<CharacterData> selectedCharacters = new List<CharacterData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンを跨いでも破棄されない
        }
        else
        {
            Destroy(gameObject); // 2つ目が出ないように
        }
    }
}