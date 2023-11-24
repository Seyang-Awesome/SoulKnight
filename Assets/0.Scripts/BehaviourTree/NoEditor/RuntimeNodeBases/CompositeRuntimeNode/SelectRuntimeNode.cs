
namespace Seyang.BehaviourTree
{
    public class SelectRuntimeNode : CompositeRuntimeNode
    {
        public int current { get; private set; }
        public override void OnStart()
        {
            current = 0;
        }

        public override void OnStop(){}

        public override NodeState OnUpdate()
        {
            NodeState state = children[current].Update();
            switch (state)
            {
                case NodeState.Failure:
                    current++;
                    if (current >= children.Count)
                        return NodeState.Failure;
                    break;
                default:
                    return state;    
            }

            return NodeState.Failure;
        }
    }
}

