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
    
    public class DebugRuntimeNode : ActionRuntimeNode
    {
        private string message;

        public DebugRuntimeNode(string message)
        {
            this.message = message;
        }
        public override void OnStart() { }

        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}