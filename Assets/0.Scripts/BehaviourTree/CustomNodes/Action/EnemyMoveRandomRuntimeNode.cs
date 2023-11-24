using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMoveRandomRuntimeNode : EnemyRuntimeNodeBase
{
    private Vector2 direction;
    public override void OnStart()
    {
        base.OnStart();
        controller.PlayAnimation(AniamtionType.Move);
        
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f); 
        direction = new Vector2(x, y).normalized;
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        
        controller.SetVelocity(direction * info.moveSpeed);
    }

    public override NodeState OnUpdate()
    {
        return NodeState.Success;
    }
}
