using UnityEngine;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    public Image buttonImage;          // ボタンに表示する画像
    public CharacterData assignedCharacter; // このボタンに割り当てられたキャラ

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
            buttonImage.sprite = character.icon; // ボタンの見た目を変更
        }
    }

    // ボタンを押したときにPrefabを召喚
    public void OnButtonPressed()
    {
        if (assignedCharacter != null && assignedCharacter.prefab != null)
        {
            if (gameDirector.money >= assignedCharacter.cost)
            {
                // キャラ召喚
                Instantiate(assignedCharacter.prefab, Vector3.zero, Quaternion.identity);
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