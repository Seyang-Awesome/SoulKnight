using System;
using System.Collections.Generic;
using UnityEngine;

public class RootRuntimeNode : RuntimeNodeBase
{
    public RuntimeNodeBase child;
    public override void OnStart() { }
    public override void OnStop() { }
    public override NodeState OnUpdate()
    {
        return child.Update();
    }
}

