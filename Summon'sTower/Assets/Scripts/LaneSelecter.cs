using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneSelector : MonoBehaviour
{
    [SerializeField] private Button[] laneButtons;  // レーンボタン配列
    private int selectedLane = -1;                  // 選択中レーン

    private Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 0.15f); // 薄グレー
    private Color unselectedColor = new Color(1f, 1f, 1f, 0f);       // 完全透明

    void Start()
    {
        // 各ボタンにクリックイベントを登録
        for (int i = 0; i < laneButtons.Length; i++)
        {
            int index = i; // クロージャ対応
            laneButtons[i].onClick.AddListener(() => SelectLane(index));
        }

        // 最初は全て透明
        selectedLane = 1;
        UpdateButtonColors();
    }

    public void SelectLane(int laneIndex)
    {
        selectedLane = laneIndex;
        UpdateButtonColors();
        Debug.Log("選択されたレーン: " + selectedLane);
    }

    private void UpdateButtonColors()
    {
        for (int i = 0; i < laneButtons.Length; i++)
        {
            Image img = laneButtons[i].GetComponent<Image>();
            if (img != null)
            {
                img.color = (i == selectedLane) ? selectedColor : unselectedColor;
            }
        }
    }

    public int GetSelectedLane()
    {
        return selectedLane;
    }
}