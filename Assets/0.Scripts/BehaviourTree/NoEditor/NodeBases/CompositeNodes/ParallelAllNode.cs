using System.Collections.Generic;
using System.Linq;

namespace MyEditor.BehaviourTree
{
    public class ParallelAllNode : CompositeNode
    {
        private List<NodeState> nodeStates;

        public override void OnStart()
        {
            nodeStates = new(children.Count);
            nodeStates.ForEach(nodeState => nodeState = NodeState.Running);
        }

        public override void OnStop()
        {

        }

        public override NodeState OnUpdate()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if(nodeStates[i] == NodeState.Success || nodeStates[i] == NodeState.Failure)
                    continue;
                nodeStates[i] = children[i].Update();
            }

            int compeletedNum = nodeStates.Count(nodeState => nodeState == NodeState.Success || 
                                                              nodeState == NodeState.Failure);
            if (compeletedNum >= children.Count)
                return NodeState.Success;
            return NodeState.Running;
        }
    }
}