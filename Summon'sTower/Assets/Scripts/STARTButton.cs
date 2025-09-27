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
                Debug.Log("パーティが揃っていません！");
                return; // 空の枠があったらマッチ不可
            }
        }

        SceneManager.LoadScene("Main Scene");
    }
}