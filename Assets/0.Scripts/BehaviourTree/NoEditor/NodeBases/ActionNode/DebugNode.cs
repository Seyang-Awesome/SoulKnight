using System;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class DebugNode : ActionNode
    {
        public string message;
        public override Type relevantType => typeof(DebugRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new DebugRuntimeNode(message);
        }
    }
}