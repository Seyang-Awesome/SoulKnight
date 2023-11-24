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

