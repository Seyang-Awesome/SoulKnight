using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MyEditor.BehaviourTree;

public class BtRunner : MonoBehaviour
{
    public BehaviourTreeConfig bt;
    private RootRuntimeNode rootRuntimeNode;
    public event Action onFixedUpdate;
    private void Start()
    {
        NodeRuntimeData nodeData = new NodeRuntimeData(this, gameObject);
        bt = bt.CloneBtTree();
        bt.nodes.ForEach(node => node.Init(nodeData));
    }

    private void FixedUpdate()
    {
        onFixedUpdate?.Invoke();
    }

    private void Update()
    {
        bt.Update();
    }

    private RuntimeNodeBase CloneBtTreeToRuntime(NodeBase root)
    {
        Type relevantType = root.relevantType;
        
    }
}
