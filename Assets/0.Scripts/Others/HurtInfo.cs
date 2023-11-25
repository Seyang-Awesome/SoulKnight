using System;
using System.Collections.Generic;
using UnityEngine;

public class HurtInfo
{
    public int Damage { get; private set; }
    public Vector2 DamageDirection { get; private set; }

    public HurtInfo(int damage, Vector2 damageSource)
    {
        Damage = damage;
        DamageDirection = damageSource;
    }
}

