using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneSelector : MonoBehaviour
{
    [SerializeField] private Button[] laneButtons;  // ���[���{�^���z��
    private int selectedLane = -1;                  // �I�𒆃��[��

    private Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 0.15f); // ���O���[
    private Color unselectedColor = new Color(1f, 1f, 1f, 0f);       // ���S����

    void Start()
    {
        // �e�{�^���ɃN���b�N�C�x���g��o�^
        for (int i = 0; i < laneButtons.Length; i++)
        {
            int index = i; // �N���[�W���Ή�
            laneButtons[i].onClick.AddListener(() => SelectLane(index));
        }

        // �ŏ��͑S�ē���
        selectedLane = 1;
        UpdateButtonColors();
    }

    public void SelectLane(int laneIndex)
    {
        selectedLane = laneIndex;
        UpdateButtonColors();
        Debug.Log("�I�����ꂽ���[��: " + selectedLane);
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