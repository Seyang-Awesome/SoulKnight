using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

public abstract class EnemyRuntimeDecoratorNodeBase : DecoratorRuntimeNode
{
    private EnemyComponentsBase components;
    protected EnemyInfo Info;
    protected EnemyControllerBase Controller;

    public override void OnStart()
    {
        if (components == null)
        {
            components = data.gameObject.GetComponent<EnemyComponentsBase>();
            Info = components.info;
            Controller = components.controller;
        }
    }

    public override void OnStop()
    {
        
    }
}

