using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonData : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int Count;

    public void OnClick()
    {
        PlayerSpawn();
    }
    void PlayerSpawn()
    {
        if (Count == 0)
            Count = 1;
        for (int i = 0; i < Count; i++)
        {
            float y = Random.Range(0.4f, -1.0f);
            Instantiate(player, new Vector3(-5.4f, y, 0), transform.rotation);
        }
    }
}
