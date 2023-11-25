using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

public abstract class EnemyRuntimeDecoratorNodeBase : DecoratorRuntimeNode
{
    private EnemyComponents components;
    protected EnemyInfo Info;
    protected EnemyController Controller;

    public override void OnStart()
    {
        if (components == null)
        {
            components = data.gameObject.GetComponent<EnemyComponents>();
            Info = components.info;
            Controller = components.controller;
        }
    }

    public override void OnStop()
    {
        
    }
}

