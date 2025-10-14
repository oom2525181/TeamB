using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
public class CharacterData: ScriptableObject
{
    public string characterName;
    public Sprite icon;              // アイコン
    public int cost;                 // コスト
    public int count;                //出現数
    public float cooldownTime = 1f;  //キャラ召喚のクールタイム
    public GameObject prefab;        // キャラのPrefab


    [Header("Ownership")]                     //Inspecterで見やすくするやつ
    public bool isOwned;                      //キャラを持ってるかどうか
    public bool DefaultCharacter = false;     //デフォルトキャラかどうか
    public int collectCount = 0;             //所持数
}