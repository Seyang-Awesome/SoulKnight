using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class ContinueNode: DecoratorNode
    {
        public float duration;
        public override Type relevantType => typeof(ContinueRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new ContinueRuntimeNode(duration);
        }
    }
    
    public class ContinueRuntimeNode : DecoratorRuntimeNode
    {
        public float duration;
        private float counter;

        public ContinueRuntimeNode(float duration)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            counter = 0;
            child.OnStart();
        }

        public override void OnStop()
        {
            child.OnStop();
        }

        public override NodeState OnUpdate()
        {
            counter += Time.deltaTime;
            if (counter <= duration)
            {
                child.OnUpdate();
                return NodeState.Running;
            }
            else
                return NodeState.Success;
        }
    }

}

