using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace MyEditor.BehaviourTree
{
    //节点的状态
    public enum NodeState
    {
        Running,//正在运行
        Success,//成功运行
        Failure//运行失败
    }

    //行为树节点基类
    [Serializable]
    public abstract class NodeBase : SerializedScriptableObject
    {
        [HideInInspector]public NodeState state = NodeState.Running;
        [HideInInspector] public bool isStarted = false;
        [ReadOnly]public string guid;
        [ReadOnly]public Vector2 posInView;
        public NodeState Update()
        {
            if (!isStarted)
            {
                isStarted = true;
                OnStart();
            }
            state = OnUpdate();
            if (state == NodeState.Success || state == NodeState.Failure)
            {
                isStarted = false;
                OnStop();
            }

            return state;
        }
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();
        public virtual NodeBase CloneNode()
        {
            NodeBase node = Instantiate(this);
            return node;
        }
    }
    
    /// <summary>
    /// 组合类节点，有多个子节点按照一定规律运行
    /// </summary>
    public abstract class CompositeNode : NodeBase
    {
        public List<NodeBase> children = new List<NodeBase>();

        public override NodeBase CloneNode()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(n => n.CloneNode());
            return node;
        }
    }

    /// <summary>
    /// 装饰类节点（条件类节点），根据条件控制子节点的运行规律
    /// </summary>
    public abstract class DecoratorNode : NodeBase
    {
        public NodeBase child;

        public override NodeBase CloneNode()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.CloneNode();
            return node;
        }
    }

    /// <summary>
    /// 行为节点，没有子节点，是具体行为执行的基类
    /// </summary>
    public abstract class ActionNode: NodeBase
    {

    }
}

