using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        var party = PartyManager.Instance.selectedParty; // PartyManager����擾

        // 1�l�ł��Z�b�g����Ă����OK
        bool hasCharacter = false;
        foreach (var c in party)
        {
            if (c != null)
            {
                hasCharacter = true;
                break;
            }
        }

        if (hasCharacter)
        {
            SceneManager.LoadScene("Main Scene");
        }
        else
        {
            Debug.Log("�Œ�1�l�L�������Z�b�g���Ă��������I");
        }
    }
}