using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seyang.BehaviourTree
{
    //节点的状态
    public enum NodeState
    {
        Running,//正在运行
        Success,//成功运行
        Failure//运行失败
    }

    public abstract class RuntimeNodeBase
    {
        protected NodeRuntimeData data;
        private NodeState state = NodeState.Running;
        private bool isStarted = false;
    
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
    }
    
    public abstract class CompositeRuntimeNode : RuntimeNodeBase
    {
        public List<RuntimeNodeBase> children = new ();
    }

    public abstract class DecoratorRuntimeNode : RuntimeNodeBase
    {
        public RuntimeNodeBase child;
    }

    public abstract class ActionRuntimeNode : RuntimeNodeBase
    {
        public override void OnStart()
        {
            data.btRunner.onFixedUpdate += OnFixedUpdate;
        }

        public override void OnStop()
        {
            data.btRunner.onFixedUpdate -= OnFixedUpdate;
        }

        protected virtual void OnFixedUpdate(){}
    }
}


