using System.Collections.Generic;
using System.Linq;

namespace MyEditor.BehaviourTree
{
    public class ParallelAnyNode : CompositeNode
    {
        private List<NodeState> nodeStates;

        protected override void OnStart()
        {
            nodeStates = new(children.Count);
            nodeStates.ForEach(nodeState => nodeState = NodeState.Running);
        }

        protected override void OnStop()
        {

        }

        protected override NodeState OnUpdate()
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