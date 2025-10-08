using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // ボタンに表示する画像
    public CharacterData assignedCharacter; // このボタンに割り当てられたキャラ
    private TextMeshProUGUI costText;         // コストの表示
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
            remaining -= 0.1f;   // 0.1秒ごとに減らす
        yield return new WaitForSeconds(0.1f); // 0.1秒待機
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
            buttonImage.sprite = character.icon; // ボタンの見た目を変更
        }
        if (costText != null && character != null)
        {
            costText.text = character.cost.ToString();
        }

    }

    // ボタンを押したときにPrefabを召喚
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
                Debug.Log($"{assignedCharacter.name} を召喚しました！");

                StartCoroutine(StartCooldown(assignedCharacter.cooldownTime));
            }
            else
            {
                Debug.Log("お金が足りません！");
            }
        }
        else
        {
            Debug.LogWarning("キャラが割り当てられてないよ！");
        }
    }
}