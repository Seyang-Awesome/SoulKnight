using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;

public abstract class EnemyRuntimeNodeBase : ActionRuntimeNode
{
    private EnemyComponentsBase components;
    protected EnemyInfo info;
    protected EnemyController controller;

    public override void OnStart()
    {
        base.OnStart();
        if (components == null)
        {
            components = data.gameObject.GetComponent<EnemyComponentsBase>();
            info = components.info;
            controller = components.controller;
        }
    }
}

