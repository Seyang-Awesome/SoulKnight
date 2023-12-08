using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class BulletInfo
{
    public Team Team { get; private set; }
    public int Damage { get; private set; }
    public Vector2 Direction { get; private set; }
    public int Offset { get; private set; }

    public BulletInfo(Team team,int damage,Vector2 direction,int offset)
    {
        Team = team;
        Damage = damage;
        Direction = direction;
        Offset = offset;
    }

    public BulletInfo(WeaponDefinitionBase wd, Vector2 direction)
    {
        Team = Team.Player;
        Damage = wd.damage;
        Offset = wd.accuracy;
        Direction = direction;
    }
}

