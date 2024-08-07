using System;
using System.Collections.Generic;
using Seyang.BehaviourTree;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class InterruptableNode : DecoratorNode
    {
        public float duration;
        public override Type relevantType => typeof(InterruptableRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new InterruptableRuntimeNode(duration);
        }
    }

    public class InterruptableRuntimeNode : DecoratorRuntimeNode
    {
        public float duration;
        private float counter;
        public InterruptableRuntimeNode(float duration)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            base.OnStart();
            counter = duration;
        }

        public override NodeState OnUpdate()
        {
            counter -= Time.deltaTime;
            if (counter <= 0 || child == null) return NodeState.Success;
            NodeState state = child.Update();
            return (state == NodeState.Success || state == NodeState.Failure) 
                ? NodeState.Success : NodeState.Running;
        }
    }
}


