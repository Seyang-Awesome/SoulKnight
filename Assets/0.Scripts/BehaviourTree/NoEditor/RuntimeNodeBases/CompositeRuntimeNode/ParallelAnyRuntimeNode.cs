using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class ParallelAnyRuntimeNode : CompositeRuntimeNode
    {
        private List<NodeState> nodeStates;

        public override void OnStart()
        {
            nodeStates = new(children.Count);
            nodeStates.ForEach(nodeState => nodeState = NodeState.Running);
        }

        public override void OnStop(){}

        public override NodeState OnUpdate()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if(nodeStates[i] == NodeState.Success || nodeStates[i] == NodeState.Failure)
                    continue;
                nodeStates[i] = children[i].Update();
            }

            if (nodeStates.Contains(NodeState.Success) || nodeStates.Contains(NodeState.Failure))
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}

