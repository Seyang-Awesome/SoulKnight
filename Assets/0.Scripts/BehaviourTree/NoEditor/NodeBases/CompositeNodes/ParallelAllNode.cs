using System;
using System.Collections.Generic;
using System.Linq;

namespace Seyang.BehaviourTree
{
    public class ParallelAllNode : CompositeNode
    {
        public override Type relevantType => typeof(ParallelAllRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new ParallelAllRuntimeNode();
        }
    }
}