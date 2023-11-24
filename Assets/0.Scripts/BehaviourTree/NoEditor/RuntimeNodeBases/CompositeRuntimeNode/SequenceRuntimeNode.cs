using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
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

