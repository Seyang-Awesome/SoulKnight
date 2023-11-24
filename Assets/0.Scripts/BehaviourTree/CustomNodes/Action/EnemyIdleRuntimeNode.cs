using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

public class EnemyIdleRuntimeNode : EnemyRuntimeNodeBase
{
    public override void OnStart()
    {
        base.OnStart();
        controller.PlayAnimation(AniamtionType.Idle);
    }

    public override void OnStop()
    {
        
    }
    
    protected override void OnFixedUpdate()
    {
        controller.SetVelocity(Vector2.zero);
    }

    public override NodeState OnUpdate()
    {
        return NodeState.Success;
    }

    
}

