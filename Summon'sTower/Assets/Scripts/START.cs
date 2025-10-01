using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class START : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void run_game()
    {
        SceneManager.LoadScene("Home");
    }
    public void edit_party()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void gacha()
    {
        SceneManager.LoadScene("Gacha");
    }

    public void Start_Battle()
    {
        var party = PartyManager.Instance.selectedParty;
        // 1人でもセットされていればOK
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
            SceneManager.LoadScene("StageSelect");
        }
        else
        {
            Debug.Log("最低1人キャラをセットしてください！");
        }
    }
}
