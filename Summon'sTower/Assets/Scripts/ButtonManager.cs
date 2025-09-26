using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // ボタンに表示する画像
    public CharacterData assignedCharacter; // このボタンに割り当てられたキャラ
    private TextMeshProUGUI costText;         // コストの表示

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
        if (assignedCharacter != null && assignedCharacter.prefab != null)
        {
            if (gameDirector.money >= assignedCharacter.cost)
            {
                if (assignedCharacter.count <= 0)
                    assignedCharacter.count = 1;                  //デフォルトだと1体出現
                for (int i = 0; i < assignedCharacter.count; i++)
                {
                    // キャラ召喚
                    float y = Random.Range(0.4f, -1.0f);          //若干y座標がランダムに出現
                    Instantiate(assignedCharacter.prefab, new Vector3(-5.7f, y, 0), transform.rotation);
                }
                Debug.Log(assignedCharacter.name + " を召喚しました！");

                // お金を消費
                gameDirector.money -= assignedCharacter.cost;
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