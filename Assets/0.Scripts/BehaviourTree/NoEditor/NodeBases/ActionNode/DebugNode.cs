using UnityEngine;

namespace MyEditor.BehaviourTree
{
    public class DebugNode : ActionNode
    {
        public string message;
        public DebugNode() { }
        public DebugNode(string message) { this.message = message; }
        public override void OnStart() { }
        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}