using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReseter : MonoBehaviour
{
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        PlayerData.AddCoin(99999);
        PlayerData.SaveStageCleared(6);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
