using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Serialization;

namespace MyEditor.BehaviourTree
{
    

    //行为树节点基类
    [Serializable]
    public abstract class NodeBase : ScriptableObject
    {
        public abstract Type relevantType { get; }

        //运行时数据
        // protected BtRunner btRunner;
        // protected GameObject gameObject;
        protected NodeRuntimeData data;
        protected NodeState state = NodeState.Running;
        protected bool isStarted = false;
        
        //存储数据
        [ReadOnly]public string guid;
        [ReadOnly]public Vector2 posInView;
        
        //运行时函数
        // public void Init(BtRunner btRunner,GameObject gameObject)
        // {
        //     this.btRunner = btRunner;
        //     this.gameObject = gameObject;
        // }
        
        //运行时函数
        public void Init(NodeRuntimeData data)
        {
            this.data = data;
        }
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
        public abstract void OnStart();
        public abstract void OnStop();
        public abstract NodeState OnUpdate();
        
        //编辑行为树函数
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
        public override void OnStart()
        {
            // Debug.Log(data);
            // Debug.Log(data.btRunner);
            // Debug.Log(data.gameObject);

            data.btRunner.onFixedUpdate += OnFixedUpdate;
        }

        public override void OnStop()
        {
            data.btRunner.onFixedUpdate -= OnFixedUpdate;
        }

        protected virtual void OnFixedUpdate() { }
    }
}

