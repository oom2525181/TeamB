using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite icon;         // アイコン
    public int cost;            // コスト
    public int count;           //出現数
    public GameObject prefab;   // キャラのPrefab
}