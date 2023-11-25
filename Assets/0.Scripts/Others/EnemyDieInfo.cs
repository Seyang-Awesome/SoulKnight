using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieInfo
{
    public Vector2 Direction { get; private set; }
    public Sprite EnemeyDieSprite { get; private set; }

    public EnemyDieInfo(Vector2 direction, Sprite enemeyDieSprite)
    {
        Direction = direction;
        EnemeyDieSprite = enemeyDieSprite;
    }
}

