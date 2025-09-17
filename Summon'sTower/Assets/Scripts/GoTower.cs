using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTower : MonoBehaviour
{
    private float speed = 5.0f;
    [SerializeField] Transform target;
    private void Start()
    {
        if(CompareTag("Ally"))
        target = GameObject.Find("Enemy'sTower").transform;
        else if (CompareTag("Enemy"))
            target = GameObject.Find("Ally'sTower").transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
