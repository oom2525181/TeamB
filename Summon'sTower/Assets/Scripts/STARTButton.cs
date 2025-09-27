using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton: MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        var party = PartyManager.Instance.selectedParty;
        foreach (var c in party)
        {
            if (c == null)
            {
                Debug.Log("�p�[�e�B�������Ă��܂���I");
                return; // ��̘g����������}�b�`�s��
            }
        }

        SceneManager.LoadScene("Main Scene");
    }
}