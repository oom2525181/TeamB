using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonData : MonoBehaviour
{
    [SerializeField] GameObject player;
public void OnClick()
    {
        PlayerSpawn();
    }
    void PlayerSpawn()
    {
        float y = Random.Range(-0.9f, -1.7f);
        Instantiate(player, new Vector3(-5.4f, y, 0), transform.rotation);
    }
}
