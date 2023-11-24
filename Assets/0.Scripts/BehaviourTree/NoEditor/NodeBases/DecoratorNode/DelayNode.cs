using System;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    /// <summary>
    /// 延迟节点，延迟一定时间后执行子节点
    /// </summary>
    public class DelayNode : DecoratorNode
    {
        public float duration;
        public override Type relevantType => typeof(DelayRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new DelayRuntimeNode(duration);
        }
    }
}