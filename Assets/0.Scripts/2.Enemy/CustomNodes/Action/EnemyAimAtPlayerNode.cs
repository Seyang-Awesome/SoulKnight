using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

public class EnemyAimAtPlayerNode : ActionNode
{
    public override Type relevantType => typeof(EnemyAimAtPlayerRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new EnemyAimAtPlayerRuntimeNode();
    }
}

public class EnemyAimAtPlayerRuntimeNode : EnemyRuntimeActionNodeBase
{
    private EnemyController_ArmedWeapon controller;
    public override void OnStart()
    {
        base.OnStart();
        controller = Controller as EnemyController_ArmedWeapon;
        controller.SetVelocity(Vector2.zero);
        Controller.PlayAnimation(AnimationType.Idle);
    }

    public override NodeState OnUpdate()
    {
        controller.AimAtTarget();
        Controller.SetFaceDirection();
        
        return NodeState.Success;
    }
}

