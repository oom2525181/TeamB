using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageButtonManager : MonoBehaviour
{
    public int stageNumber;         // このボタンが対応するステージ番号
    public Button button;
    public TextMeshProUGUI text; 

    private void Start()
    {
        //// ボタンが押せるか判定
        //if (PlayerData.IsStageCleared(stageNumber - 1)) // 前のステージをクリアしていれば押せる
        //{
        //    button.interactable = true;
        //}
        //else
        //{
        //    button.interactable = false;
        //}

        Debug.Log($"[StageButtonManager] 最後にクリアしたステージ = {PlayerData.GetLastClearedStage()}");

        // 前のステージをクリアしていれば表示
        if (PlayerData.IsStageCleared(stageNumber - 1))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false); // 非表示
        }

        // ボタンに表示するテキスト
        if (text != null)
            text.text = "Stage " + stageNumber;
    }

    public void OnButtonPressed()
    {
        if (!button.interactable)
            return;

        // ボタンを押したらステージをロード
        //Debug.Log("ステージ " + stageNumber + " を開始");
        SceneManager.LoadScene("Stage" + stageNumber);
    }
}