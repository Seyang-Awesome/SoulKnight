using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;

public class EnemyIdleNode : ActionNode
{
    public override Type relevantType => typeof(EnemyIdleRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new EnemyIdleRuntimeNode();
    }
}

public class EnemyIdleRuntimeNode : EnemyRuntimeActionNodeBase
{
    public override void OnStart()
    {
        base.OnStart();
        Controller.PlayAnimation(AnimationType.Idle);
        Controller.SetVelocity(Vector2.zero);
    }

    public override void OnStop()
    {
        
    }
    
    protected override void OnFixedUpdate()
    {
    }

    public override NodeState OnUpdate()
    {
        return NodeState.Running;
    }
}

