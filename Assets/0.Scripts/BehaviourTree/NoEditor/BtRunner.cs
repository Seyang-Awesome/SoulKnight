using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Seyang.BehaviourTree;

public class BtRunner : MonoBehaviour
{
    public BehaviourTreeConfig bt;
    private RootRuntimeNode rootRuntimeNode;
    private NodeRuntimeData nodeData;
    public event Action onFixedUpdate;
    
    private void Start()
    {
        bt = Instantiate(bt); 
        nodeData = new NodeRuntimeData(this, gameObject);
        rootRuntimeNode = CloneBtTreeToRuntime(bt.rootNode) as RootRuntimeNode;
        
    }

    private void FixedUpdate()
    {
        onFixedUpdate?.Invoke();
    }

    private void Update()
    {
        rootRuntimeNode.Update();
    }

    private RuntimeNodeBase CloneBtTreeToRuntime(NodeBase rootNode)
    {
        if (rootNode == null) return null;
        
        RuntimeNodeBase runtimeNode = null;

        runtimeNode = rootNode.InstantiateRuntimeNode();
        runtimeNode.Init(nodeData);

        if (runtimeNode is RootRuntimeNode rootRuntimeNode)
        {
            rootRuntimeNode.child = CloneBtTreeToRuntime(((RootNode)rootNode).child);
        }
        else if (runtimeNode is CompositeRuntimeNode compositeRuntimeNode)
        {
            ((CompositeNode)rootNode).children.ForEach(node => compositeRuntimeNode.children.Add(
                CloneBtTreeToRuntime(node)));
        }
        else if (runtimeNode is DecoratorRuntimeNode decoratorRuntimeNode)
        {
            decoratorRuntimeNode.child = CloneBtTreeToRuntime(((DecoratorNode)rootNode).child);
        }
        
        return runtimeNode;

    }
}
