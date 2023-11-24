using System;
using System.Collections.Generic;
using MyEditor.BehaviourTree;
using UnityEngine;

public class EnemyIdleNode : EnemyNodeBase
{
    public override void OnStart()
    {
        base.OnStart();
        controller.PlayAnimation(AniamtionType.Idle);
    }
    
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        controller.SetVelocity(Vector2.zero);
        Debug.Log("OnFixedUpdate");
    }

    public override NodeState OnUpdate()
    {
        return NodeState.Running;
    }
    
    public override void OnStop()
    {
        base.OnStop();
    }
}

