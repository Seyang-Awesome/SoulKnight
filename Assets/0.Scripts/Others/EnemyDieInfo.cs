using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieInfo
{
    public Vector2 Position { get; private set; }
    public Vector2 Direction { get; private set; }
    public Sprite EnemyDieSprite { get; private set; }
    public GameObject EnemyGameObject { get; private set; }

    public EnemyDieInfo(Vector2 position,Vector2 direction, Sprite enemyDieSprite,GameObject enemyGameObject)
    {
        Position = position;
        Direction = direction;
        EnemyDieSprite = enemyDieSprite;
        EnemyGameObject = enemyGameObject;
    }
}

