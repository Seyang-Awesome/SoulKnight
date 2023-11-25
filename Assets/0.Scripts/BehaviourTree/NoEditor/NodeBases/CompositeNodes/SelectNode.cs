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
