using System;
using System.Collections.Generic;
using System.Linq;

namespace Seyang.BehaviourTree
{
    public class ParallelAnyNode : CompositeNode
    {
        public override Type relevantType => typeof(ParallelAnyRuntimeNode);
        public override RuntimeNodeBase InstantiateRuntimeNode()
        {
            return new ParallelAnyRuntimeNode();
        }
    }
}