using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Serialization;

namespace Seyang.BehaviourTree
{
    //行为树节点基类
    [Serializable]
    public abstract class NodeBase : ScriptableObject
    {
        public abstract Type relevantType { get; }
        
        //存储数据
        [ReadOnly]public string guid;
        [ReadOnly]public Vector2 posInView;
        
        //编辑行为树函数
        public virtual NodeBase CloneNode()
        {
            NodeBase node = Instantiate(this);
            return node;
        }

        public abstract RuntimeNodeBase InstantiateRuntimeNode();
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
        public override NodeBase CloneNode()
        {
            return Instantiate(this);
        }
    }
}

