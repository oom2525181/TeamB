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


    private void Start()
    {
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
                if (assignedCharacter.count <= 0)
                    assignedCharacter.count = 1;

                for (int i = 0; i < assignedCharacter.count; i++)
                {
                    float y = Random.Range(-0.88f, -0.48f);
                    Instantiate(assignedCharacter.prefab, new Vector3(-7.2f, y, 0), transform.rotation);
                }

                gameDirector.money -= assignedCharacter.cost;
                Debug.Log($"{assignedCharacter.name} ���������܂����I");

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