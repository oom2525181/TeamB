using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageButtonManager : MonoBehaviour
{
    public int stageNumber;         // ���̃{�^�����Ή�����X�e�[�W�ԍ�
    public Button button;
    public TextMeshProUGUI text; 

    private void Start()
    {
        //// �{�^���������邩����
        //if (PlayerData.IsStageCleared(stageNumber - 1)) // �O�̃X�e�[�W���N���A���Ă���Ή�����
        //{
        //    button.interactable = true;
        //}
        //else
        //{
        //    button.interactable = false;
        //}

        Debug.Log($"[StageButtonManager] �Ō�ɃN���A�����X�e�[�W = {PlayerData.GetLastClearedStage()}");

        // �O�̃X�e�[�W���N���A���Ă���Ε\��
        if (PlayerData.IsStageCleared(stageNumber - 1))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false); // ��\��
        }

        // �{�^���ɕ\������e�L�X�g
        if (text != null)
            text.text = "Stage " + stageNumber;
    }

    public void OnButtonPressed()
    {
        if (!button.interactable)
            return;

        // �{�^������������X�e�[�W�����[�h
        //Debug.Log("�X�e�[�W " + stageNumber + " ���J�n");
        SceneManager.LoadScene("Stage" + stageNumber);
    }
}