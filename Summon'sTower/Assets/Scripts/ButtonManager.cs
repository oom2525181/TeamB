using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // �{�^���ɕ\������摜
    public CharacterData assignedCharacter; // ���̃{�^���Ɋ��蓖�Ă�ꂽ�L����
    private TextMeshProUGUI costText;         // �R�X�g�̕\��

    private GameDirector gameDirector;


    private void Start()
    {
        costText = GetComponentInChildren<TextMeshProUGUI>();

        SetCharacter(assignedCharacter);

        if (gameDirector == null)
            gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetCharacter(assignedCharacter);
        }

    }


    public void SetCharacter(CharacterData character)
    {
        assignedCharacter = character;
        if (buttonImage != null && character != null)
        {
            buttonImage.sprite = character.icon; // �{�^���̌����ڂ�ύX
        }
        if (costText != null && character != null)
        {
            costText.text = character.cost.ToString();
        }

    }

    // �{�^�����������Ƃ���Prefab������
    public void OnButtonPressed()
    {
        if (assignedCharacter != null && assignedCharacter.prefab != null)
        {
            if (gameDirector.money >= assignedCharacter.cost)
            {
                if (assignedCharacter.count <= 0)
                    assignedCharacter.count = 1;                  //�f�t�H���g����1�̏o��
                for (int i = 0; i < assignedCharacter.count; i++)
                {
                    // �L��������
                    float y = Random.Range(0.4f, -1.0f);          //�኱y���W�������_���ɏo��
                    Instantiate(assignedCharacter.prefab, new Vector3(-5.7f, y, 0), transform.rotation);
                }
                Debug.Log(assignedCharacter.name + " ���������܂����I");

                // ����������
                gameDirector.money -= assignedCharacter.cost;
            }
            else
            {
                Debug.Log("����������܂���I");
            }
        }
        else
        {
            Debug.LogWarning("�L���������蓖�Ă��ĂȂ���I");
        }
    }
}