using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    private float speed = 5.0f;
    [SerializeField] Transform target;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
