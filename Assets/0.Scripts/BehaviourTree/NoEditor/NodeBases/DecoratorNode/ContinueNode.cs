using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class ContinueNode: DecoratorNode
    {
        public float duration;
        public override Type relevantType => typeof(ContinueRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new ContinueRuntimeNode(duration);
        }
    }

}

