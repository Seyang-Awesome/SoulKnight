using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponents : MonoBehaviour
{
    public EnemyInfo info;
    public EnemyController controller;

    private void Awake()
    {
        info = GetComponent<EnemyInfo>();
        controller = GetComponent<EnemyController>();
    }
}

