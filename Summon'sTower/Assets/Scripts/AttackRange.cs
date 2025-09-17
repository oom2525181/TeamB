using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public bool Attack = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Attack = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Attack = false;
    }
}
