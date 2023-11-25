using System;
using System.Collections.Generic;
using UnityEngine;
using Seyang.BehaviourTree;

public class DetectEnemyNode : DecoratorNode
{
    public override Type relevantType => typeof(DetectEnemyRuntimeNode);
    public override RuntimeNodeBase InstantiateRuntimeNode()
    {
        return new DetectEnemyRuntimeNode();
    }
}

public class DetectEnemyRuntimeNode : EnemyRuntimeDecoratorNodeBase
{
    private bool isDetectPlayer;
    public override void OnStart()
    {
        base.OnStart();
        isDetectPlayer = Controller.DetectPlayer();
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    public override NodeState OnUpdate()
    {
        if (!isDetectPlayer) return NodeState.Failure;
        return child.Update();
    }
}

