using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;

public abstract class EnemyRuntimeActionNodeBase : ActionRuntimeNode
{
    private EnemyComponentsBase components;
    protected EnemyInfo Info;
    protected EnemyControllerBase Controller;

    public override void OnStart()
    {
        base.OnStart();
        if (components == null)
        {
            components = data.gameObject.GetComponent<EnemyComponentsBase>();
            Info = components.info;
            Controller = components.controller;
        }
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    protected Vector2 AdjustMoveDirection(Vector2 direction)
    {
        int directionInfo = Controller.GetWallDirection();
        if ((directionInfo & (int)Direction.Up) != 0 || (directionInfo & (int)Direction.Down) != 0)
            direction = new Vector2(direction.x, -direction.y);
        if ((directionInfo & (int)Direction.Left) != 0 || (directionInfo & (int)Direction.Right) != 0)
            direction = new Vector2(-direction.x, direction.y);
        return direction;
    }
}

