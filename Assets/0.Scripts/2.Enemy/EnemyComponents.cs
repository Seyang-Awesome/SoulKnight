using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponents : MonoBehaviour
{
    [HideInInspector] public EnemyInfo info;
    [HideInInspector] public EnemyController controller;

    private void Awake()
    {
        info = GetComponent<EnemyInfo>();
        controller = GetComponent<EnemyController>();
    }
}

