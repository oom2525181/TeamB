using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite icon;         // �A�C�R��
    public int cost;            // �R�X�g
    public GameObject prefab;   // �L������Prefab
}