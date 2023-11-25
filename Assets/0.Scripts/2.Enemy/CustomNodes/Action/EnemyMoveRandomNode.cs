using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMoveRandomNode : ActionNode
{
    public override Type relevantType => typeof(EnemyMoveRandomRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new EnemyMoveRandomRuntimeNode();
    }
}

public class EnemyMoveRandomRuntimeNode : EnemyRuntimeActionNodeBase
{
    private Vector2 direction;
    public override void OnStart()
    {
        base.OnStart();
        Controller.PlayAnimation(AnimationType.Move);

        direction = SeyangExtension.GetRandomDirection();
        direction = AdjustMoveDirection(direction);
        Controller.SetVelocity(direction * Info.moveSpeed);
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        
    }

    protected override void OnCollisionEnter(Collision2D _)
    {
        direction = AdjustMoveDirection(direction);
        Controller.SetVelocity(direction * Info.moveSpeed);
    }

    protected override void OnTriggerEnter(Collider2D collider)
    {
        direction = SeyangExtension.GetRandomDirection();
        Controller.SetVelocity(direction * Info.moveSpeed);
    }

    public override NodeState OnUpdate()
    {
        return NodeState.Running;
    }

    private void AdjustMoveDirection()
    {
        
    }
}


