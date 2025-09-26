using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // �{�^���ɕ\������摜
    public CharacterData assignedCharacter; // ���̃{�^���Ɋ��蓖�Ă�ꂽ�L����

    private GameDirector gameDirector;


    private void Start()
    {
        SetCharacter(assignedCharacter);

        if (gameDirector == null)
            gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    public void SetCharacter(CharacterData character)
    {
        assignedCharacter = character;
        if (buttonImage != null && character != null)
        {
            buttonImage.sprite = character.icon; // �{�^���̌����ڂ�ύX
        }
    }

    // �{�^�����������Ƃ���Prefab������
    public void OnButtonPressed()
    {
        if (assignedCharacter != null && assignedCharacter.prefab != null)
        {
            if (gameDirector.money >= assignedCharacter.cost)
            {
                // �L��������
                Instantiate(assignedCharacter.prefab, Vector3.zero, Quaternion.identity);
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