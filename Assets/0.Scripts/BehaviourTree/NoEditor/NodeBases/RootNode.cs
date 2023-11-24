using System;
using UnityEngine;

namespace MyEditor.BehaviourTree
{
    public class RootNode : NodeBase
    {
        public NodeBase child;
        public override Type relevantType => typeof(RootRuntimeNode);
        public override void OnStart() { }
        public override void OnStop() { }
        public override NodeState OnUpdate()
        {
            return child.Update();
        }
        public override NodeBase CloneNode()
        {
            RootNode node = Instantiate(this);
            node.child = child.CloneNode();
            return node;
        }
    }
}
