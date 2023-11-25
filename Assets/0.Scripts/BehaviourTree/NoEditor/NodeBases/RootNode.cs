using System;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class RootNode : NodeBase
    {
        public NodeBase child;
        public override Type relevantType => typeof(RootRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new RootRuntimeNode();
        }
    }
    
    public class RootRuntimeNode : RuntimeNodeBase
    {
        public RuntimeNodeBase child;

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeState OnUpdate()
        {
            return child.Update();
        }
    }
}
