using System;
using System.Collections.Generic;
using UnityEngine;

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
    protected NodeState state = NodeState.Running;
    protected bool isStarted = false;
    
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

