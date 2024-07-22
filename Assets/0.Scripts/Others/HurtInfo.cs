using System;
using System.Collections.Generic;
using UnityEngine;

public struct HurtInfo
{
    public int Damage { get; private set; }
    public Vector2 DamageDirection { get; private set; }
    public float DamageIntensity { get; private set; }

    public HurtInfo(int damage, Vector2 damageSource,float damageIntensity)
    {
        Damage = damage;
        DamageDirection = damageSource.normalized;
        DamageIntensity = damageIntensity;
    }
}

