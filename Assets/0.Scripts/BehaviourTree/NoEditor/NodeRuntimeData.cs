using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeRuntimeData
{
    public BtRunner btRunner;
    public GameObject gameObject;

    public NodeRuntimeData(BtRunner btRunner, GameObject gameObject)
    {
        this.btRunner = btRunner;
        this.gameObject = gameObject;
    }
}

