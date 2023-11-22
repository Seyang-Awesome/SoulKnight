using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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
        [HideInInspector] public Vector2 posInView;
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
        public virtual NodeBase CloneNode()
        {
            NodeBase node = Instantiate(this);
            return node;
        }
    }

    public class RootNode : NodeBase
    {
        public NodeBase child;
        public override void OnStart() { }

        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            return child.Update();
        }

        public override NodeBase CloneNode()
        {
            RootNode node = Instantiate(this);
            node.child = child.CloneNode();
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

    /// <summary>
    /// 顺序节点，会按照子节点的次序一个接一个执行
    /// 全部执行完毕后返回Success
    /// 若其中有一个执行失败返回Failure
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        int current;
        public override void OnStart()
        {
            current = 0;
        }

        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    current++;
                    if (current >= children.Count)
                        return NodeState.Success;
                    return NodeState.Running;
                case NodeState.Failure:
                    return NodeState.Failure;
                default:
                    throw new System.Exception("行为树运行状态出现异常！");
            }
        }
    }

    /// <summary>
    /// 循环节点，表明该节点的子节点会一直执行（一般作为根节点）
    /// </summary>
    public class LoopNode : DecoratorNode
    {
        public override void OnStart() { }

        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            child.Update();
            return NodeState.Running;
        }
    }

    /// <summary>
    /// 延迟节点，延迟一定时间后执行子节点
    /// </summary>
    public class DelayNode : DecoratorNode
    {
        public float duration;
        float currentTime;
        public DelayNode() { }
        public DelayNode(float duration) { this.duration = duration; }

        public override void OnStart()
        {
            currentTime = 0;
        }

        public override void OnStop() { }

        public override NodeState OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime <= duration)
                return NodeState.Running;
            else
                return child.Update();
        }
    }

    public class DebugNode : ActionNode
    {
        public string message;
        public DebugNode() { }
        public DebugNode(string message) { this.message = message; }
        public override void OnStart() { }
        public override void OnStop() { }
        public override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }
    }
}

