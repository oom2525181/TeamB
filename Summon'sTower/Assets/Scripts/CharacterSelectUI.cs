using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    public ButtonManager[] partyButtons; // Inspector�Ń{�^�������蓖�Ă�

    public void SetParty(CharacterData[] selectedParty)
    {
        for (int i = 0; i < selectedParty.Length; i++)
        {
            partyButtons[i].SetCharacter(selectedParty[i]);
        }
    }
}
