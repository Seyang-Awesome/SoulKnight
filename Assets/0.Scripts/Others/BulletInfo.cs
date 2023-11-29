using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class BulletInfo
{
    public int Damage { get; private set; }
    public Vector2 Direction { get; private set; }
    public int Offset { get; private set; }

    public BulletInfo(int damage,Vector2 direction,int offset)
    {
        Damage = damage;
        Direction = direction;
        Offset = offset;
    }

    public BulletInfo(WeaponDefinitionBase wd, Vector2 direction)
    {
        Damage = wd.damage;
        Offset = wd.accuracy;
        Direction = direction;
    }
}

