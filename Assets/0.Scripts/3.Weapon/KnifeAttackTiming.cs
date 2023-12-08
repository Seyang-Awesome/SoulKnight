using System;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttackTiming : MonoBehaviour
{
    private WeaponInGame_Knife controller;
    public void Init()
    {
        controller = transform.parent.GetComponent<WeaponInGame_Knife>();
    }

    private void OnAttackTiming()
    {
        controller.OnAttackTiming();
    }
}

