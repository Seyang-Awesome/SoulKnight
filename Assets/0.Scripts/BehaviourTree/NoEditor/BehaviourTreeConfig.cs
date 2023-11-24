using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;

namespace Seyang.BehaviourTree
{
    [CreateAssetMenu()]
    public class BehaviourTreeConfig : SerializedScriptableObject
    {
        [SerializeField]
        public NodeBase rootNode;

        [SerializeField]
        public List<NodeBase> nodes = new();

        public NodeBase CreateNode(System.Type type)
        {
            NodeBase newNode = ScriptableObject.CreateInstance(type) as NodeBase;
            newNode.name = type.Name;
            newNode.guid = Guid.NewGuid().ToString();
            nodes.Add(newNode);

            return newNode;
        }

        public void DeleteNode(NodeBase node)
        {
            nodes.Remove(node);
            if(node is RootNode)
            {
                rootNode = null;
            }
        }

        public void AddChild(NodeBase parent,NodeBase child)
        {
            if (parent == null || child == null) return;

            if(parent is CompositeNode)
            {
                (parent as CompositeNode).children.Add(child);
            }
            else if(parent is DecoratorNode)
            {
                (parent as DecoratorNode).child = child;
            }
            else if (parent is RootNode)
            {
                (parent as RootNode).child = child;
            }
        }

        public void RemoveChild(NodeBase parent, NodeBase child)
        {

            if (parent == null || child == null) return;

            if (parent is CompositeNode)
            {
                (parent as CompositeNode).children.Remove(child);
            }
            else if (parent is DecoratorNode)
            {
                (parent as DecoratorNode).child = null;
            }
            else if (parent is RootNode)
            {
                (parent as RootNode).child = null;
            }
        }

        public List<NodeBase> GetChildren(NodeBase parent)
        {
            List<NodeBase> nodes = new List<NodeBase>();
            if (parent == null) return nodes;

            if (parent is CompositeNode)
            {
                CompositeNode node = (parent as CompositeNode);
                if(!node.children.IsNullOrEmpty())
                    nodes = node.children;
            }
            else if (parent is DecoratorNode)
            {
                DecoratorNode node = (parent as DecoratorNode);
                if(node.child != null)
                    nodes.Add(node.child);
            }
            else if (parent is RootNode)
            {
                RootNode node = (parent as RootNode);
                if (node.child != null)
                    nodes.Add(node.child);
            }

            return nodes;
        }

        public BehaviourTreeConfig CloneBtTree()
        {
            BehaviourTreeConfig btConfig = Instantiate(this);
            btConfig.rootNode = rootNode.CloneNode();
            return btConfig;
        }
    }
}
