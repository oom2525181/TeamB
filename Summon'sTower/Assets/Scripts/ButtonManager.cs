using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // �{�^���ɕ\������摜
    public CharacterData assignedCharacter; // ���̃{�^���Ɋ��蓖�Ă�ꂽ�L����
    private TextMeshProUGUI costText;         // �R�X�g�̕\��
    private Button button;

    private GameDirector gameDirector;

    private TextMeshProUGUI cooldownText;
    private bool onCooldown = false;

    public LaneSelector laneSelector;

    private void Start()
    {
        // LaneSelector ���V�[�������玩���擾
        if (laneSelector == null)
            laneSelector = FindFirstObjectByType<LaneSelector>();

        button = GetComponent<Button>();
        cooldownText = transform.Find("CooldownText")?.GetComponent<TextMeshProUGUI>();

        costText = GetComponentInChildren<TextMeshProUGUI>();

        SetCharacter(assignedCharacter);

        if (gameDirector == null)
            gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    private IEnumerator StartCooldown(float cooldown)
    {
        onCooldown = true;
        if (button != null) button.interactable = false;

         float remaining = cooldown;
    while (remaining > 0)
    {
            cooldownText.text = remaining.ToString("0.0");
            remaining -= 0.1f;   // 0.1�b���ƂɌ��炷
        yield return new WaitForSeconds(0.1f); // 0.1�b�ҋ@
    }

        if (button != null) button.interactable = true;
        if (cooldownText != null)
            cooldownText.text = "";

        onCooldown = false;
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
        if (onCooldown) return;

        if (assignedCharacter != null && assignedCharacter.prefab != null)
        {
            if (gameDirector.money >= assignedCharacter.cost)
            {
                int lane = 1; // �f�t�H���g
                if (laneSelector != null)
                {
                    lane = laneSelector.GetSelectedLane();
                    if (lane < 0)
                    {
                        Debug.LogWarning("���[�����I������Ă��܂���I");
                        return;
                    }
                }

                // �I�����ꂽ���[���ɑΉ�����Y���W�����߂�
                float y = 0f;
                switch (lane)
                {
                    case 0: y =  2.2f; break;
                    case 1: y = -1.2f; break;
                    case 2: y =  -2.5f; break;
                        // �K�v�ɉ����Ēǉ�
                }

                for (int i = 0; i < assignedCharacter.count; i++)
                {
                    Instantiate(assignedCharacter.prefab, new Vector3(-7.2f, y, 0), Quaternion.identity);
                }

                gameDirector.money -= assignedCharacter.cost;
                Debug.Log($"{assignedCharacter.name} �����[�� {lane} �ɏ������܂����I");

                StartCoroutine(StartCooldown(assignedCharacter.cooldownTime));
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