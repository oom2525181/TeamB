using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite icon;         // アイコン
    public int cost;            // コスト
    public GameObject prefab;   // キャラのPrefab
}