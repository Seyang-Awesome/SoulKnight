using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;

public class EnemyAttackNode : ActionNode
{
    public override Type relevantType => typeof(EnemyAttackRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new EnemyAttackRuntimeNode();
    }
}

public class EnemyAttackRuntimeNode : EnemyRuntimeActionNodeBase
{
    public override void OnStart()
    {
        base.OnStart();
        Controller.PlayAnimation(AnimationType.Idle);
    }

    public override NodeState OnUpdate()
    {
        Controller.Attack();
        return NodeState.Success;
    }
}

