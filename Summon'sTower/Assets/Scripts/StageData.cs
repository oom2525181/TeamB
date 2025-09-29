using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/StageConfig")]
public class StageConfig : ScriptableObject
{
    public Wave[] waves;
}