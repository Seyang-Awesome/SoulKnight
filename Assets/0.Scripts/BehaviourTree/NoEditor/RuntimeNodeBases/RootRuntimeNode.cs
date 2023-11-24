using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    public class RootRuntimeNode : RuntimeNodeBase
    {
        public RuntimeNodeBase child;

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override NodeState OnUpdate()
        {
            return child.Update();
        }
    }
}

