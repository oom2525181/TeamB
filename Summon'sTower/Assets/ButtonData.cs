using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonData : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int Count;
    [SerializeField] int Cost;

    public EnemySpawner spawner;

    GameObject director;
    public GameDirector gameDirector;

    void Start()
    {
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        this.director = GameObject.Find("GameDirector");
        gameDirector = director.GetComponent<GameDirector>();
    }

    public void OnClick()
    {
        PlayerSpawn();
    }
    void PlayerSpawn()
    {
        if (Count == 0)
            Count = 1;
        if (spawner.isSTOPED == false && gameDirector.money >= Cost)
        {
            gameDirector.money -= Cost;
            for (int i = 0; i < Count; i++)
            {
                float y = Random.Range(0.4f, -1.0f);
                Instantiate(player, new Vector3(-5.7f, y, 0), transform.rotation);
            }
        }
    }
}
