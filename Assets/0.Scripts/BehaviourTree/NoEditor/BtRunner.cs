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
    public event Action OnFixedUpdate;
    public event Action<Collision2D> OnCollisionEnter;
    public event Action<Collision2D> OnCollisionExit;
    public event Action<Collider2D> OnTriggerEnter;
    public event Action<Collider2D> OnTriggerExit;
    
    private void Start()
    {
        bt = Instantiate(bt); 
        nodeData = new NodeRuntimeData(this, gameObject);
        rootRuntimeNode = CloneBtTreeToRuntime(bt.rootNode) as RootRuntimeNode;
        
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    private void Update()
    {
        rootRuntimeNode.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit?.Invoke(other);
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
