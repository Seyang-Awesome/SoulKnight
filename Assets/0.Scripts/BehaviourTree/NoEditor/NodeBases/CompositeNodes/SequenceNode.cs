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
}