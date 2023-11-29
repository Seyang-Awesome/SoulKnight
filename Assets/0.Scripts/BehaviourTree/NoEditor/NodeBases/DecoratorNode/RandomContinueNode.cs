using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Seyang.BehaviourTree
{
    public class RandomContinueNode : DecoratorNode
    {
        public float durationMin;
        public float durationMax;

        public override Type relevantType => typeof(RandomContinueRuntimeNode);

        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new RandomContinueRuntimeNode(durationMin,durationMax);
        }
    }

    public class RandomContinueRuntimeNode : DecoratorRuntimeNode
    {
        private float durationMin;
        private float durationMax;
        
        private float duration;
        private float counter;

        public RandomContinueRuntimeNode(float durationMin, float durationMax)
        {
            this.durationMin = durationMin;
            this.durationMax = durationMax;
        }

        public override void OnStart()
        {
            counter = 0;
            duration = Random.Range(durationMin, durationMax);
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
                child?.OnUpdate();
                return NodeState.Running;
            }
            else
                return NodeState.Success;
        }
    }
}
