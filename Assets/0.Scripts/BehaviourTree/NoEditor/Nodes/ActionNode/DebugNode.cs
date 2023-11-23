using UnityEngine;

namespace MyEditor.BehaviourTree
{
    public class DebugNode : ActionNode
    {
        public string message;
        public DebugNode() { }
        public DebugNode(string message) { this.message = message; }
        protected override void OnStart() { }
        protected override void OnStop() { }
        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}