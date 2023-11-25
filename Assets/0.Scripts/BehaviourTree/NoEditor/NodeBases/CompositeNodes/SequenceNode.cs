using System;

namespace Seyang.BehaviourTree
{
    /// <summary>
    /// 顺序节点，会按照子节点的次序一个接一个执行
    /// 全部执行完毕后返回Success
    /// 若其中有一个执行失败返回Failure
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        public override Type relevantType => typeof(SequenceRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new SequenceRuntimeNode();
        }
    }
    
    public class SequenceRuntimeNode : CompositeRuntimeNode
    {
        public int current;
        public override void OnStart()
        {
            current = 0;
        }

        public override void OnStop(){}

        public override NodeState OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    current++;
                    if (current >= children.Count)
                        return NodeState.Success;
                    return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Failure;
                default:
                    throw new System.Exception("行为树运行状态出现异常！");
            }
        }
    }
}