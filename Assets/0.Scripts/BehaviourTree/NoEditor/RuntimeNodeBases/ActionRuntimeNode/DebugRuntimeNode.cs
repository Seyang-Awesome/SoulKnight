using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
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

