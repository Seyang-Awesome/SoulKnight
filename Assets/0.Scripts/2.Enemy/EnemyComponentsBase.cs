using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponentsBase : MonoBehaviour
{
    [SerializeField] public EnemyInfo info;
    [SerializeField] public EnemyController controller;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
}

