using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game/Character")]
public class CharacterData: ScriptableObject
{
    public string characterName;
    public Sprite icon;              // �A�C�R��
    public int cost;                 // �R�X�g
    public int count;                //�o����
    public float cooldownTime = 1f;  //�L���������̃N�[���^�C��
    public GameObject prefab;        // �L������Prefab


    [Header("Ownership")]                     //Inspecter�Ō��₷��������
    public bool isOwned;                      //�L�����������Ă邩�ǂ���
    public bool DefaultCharacter = false;     //�f�t�H���g�L�������ǂ���
    public int collectCount = 0;             //������
}