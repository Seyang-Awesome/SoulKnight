using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    /// <summary>
    /// 选择节点
    /// 只进行一个节点，
    /// 若其中有一个执行成功返回Success
    /// 否则返回Failure
    /// </summary>
    public class SelectNode : CompositeNode
    {
        public override Type relevantType => typeof(SelectRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new SelectRuntimeNode();
        }
    }
}
