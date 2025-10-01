using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stage1()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void Stage2()
    {
        SceneManager.LoadScene("Stage2 Scene");
    }
    public void Stage3()
    {
        SceneManager.LoadScene("Stage3 Scene");
    }
}
