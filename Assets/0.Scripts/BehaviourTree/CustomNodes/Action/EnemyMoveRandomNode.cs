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


