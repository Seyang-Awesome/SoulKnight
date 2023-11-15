using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SeyangExtension
{
    // public async static UniTask DoBounceMove(this Transform transform, Vector3 pos, float duration)
    // {
    //     Vector3 overPos = transform.position + (pos - transform.position) * 1.1f;
    //     float overTime = duration * 1f / 2f;
    //     float backTime = duration * 1f / 2f;
    //
    //     transform.DOMove(overPos, overTime);
    //     await UniTask.Delay(TimeSpan.FromSeconds(overTime));
    //     transform.DOMove(pos, backTime);
    //     await UniTask.Delay(TimeSpan.FromSeconds(backTime));
    // }
    
}

#region InvokableAction

public interface InvokableActionBase
{

}

public class InvokableAction : InvokableActionBase
{
    public event Action action;
    public void Invoke() => action?.Invoke();

    public static InvokableAction operator +(InvokableAction thisInvokableAction, Action action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction operator -(InvokableAction thisInvokableAction, Action action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
}

public class InvokableAction<T>: InvokableActionBase
{
    public event Action<T> action;
    public void Invoke(T arg) => action?.Invoke(arg);
    
    public static InvokableAction<T> operator +(InvokableAction<T> thisInvokableAction, Action<T> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T> operator -(InvokableAction<T> thisInvokableAction, Action<T> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
}

public class InvokableAction<T1, T2>: InvokableActionBase
{
    public event Action<T1,T2> action;
    public void Invoke(T1 arg1, T2 arg2) => action?.Invoke(arg1,arg2);
    
    public static InvokableAction<T1,T2> operator +(InvokableAction<T1,T2> thisInvokableAction, Action<T1,T2> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T1,T2> operator -(InvokableAction<T1,T2> thisInvokableAction, Action<T1,T2> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
}

public class InvokableAction<T1, T2, T3>: InvokableActionBase
{
    public event Action<T1,T2, T3> action;
    public void Invoke(T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg1,arg2,arg3);
    
    public static InvokableAction<T1,T2, T3> operator +(InvokableAction<T1,T2, T3> thisInvokableAction, Action<T1,T2, T3> action)
    {
        thisInvokableAction.action += action;
        return thisInvokableAction;
    }
    public static InvokableAction<T1,T2, T3> operator -(InvokableAction<T1,T2, T3> thisInvokableAction, Action<T1,T2, T3> action)
    {
        thisInvokableAction.action -= action;
        return thisInvokableAction;
    }
}
#endregion