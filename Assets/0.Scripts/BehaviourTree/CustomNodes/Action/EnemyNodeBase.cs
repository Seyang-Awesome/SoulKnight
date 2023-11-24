using System;
using System.Collections.Generic;
using MyEditor.BehaviourTree;
using UnityEngine;

public abstract class EnemyNodeBase : ActionNode
{
    protected EnemyComponentsBase enemyComponents;
    protected EnemyInfo info;
    protected EnemyController controller;

    public override void OnStart()
    {
        base.OnStart();
        if (enemyComponents != null) return;

        enemyComponents = data.gameObject.GetComponent<EnemyComponentsBase>();
        info = enemyComponents.info;
        controller = enemyComponents.controller;
    }
}

