using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class DelayRuntimeNode : DecoratorRuntimeNode
    {
        public float duration;
        private float currentTime;

        public DelayRuntimeNode(float duration)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            currentTime = 0;
        }

        public override void OnStop(){}

        public override NodeState OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime <= duration)
                return NodeState.Running;
            else
                return child.Update();
        }
    }
}

