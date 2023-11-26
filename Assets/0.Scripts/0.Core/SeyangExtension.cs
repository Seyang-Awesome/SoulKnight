using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    public static List<Type> GetDerivedClasses(this Type type)
    {
        List<Type> derivedClasses = new List<Type>();
            
        foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var t in assembly.GetTypes())
            {
                if(t.IsClass && !t.IsAbstract && type.IsAssignableFrom(t))
                {
                    derivedClasses.Add(t);
                }
            }
        }
        return derivedClasses;
    }

    public static Vector2 GetRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f); 
        return new Vector2(x, y).normalized;
    }

    public static Vector2 GetRelevantDirection(this Direction direction)
    {
        if (direction == Direction.Up)
            return Vector2.up;
        else if (direction == Direction.Down)
            return Vector2.down;
        else if (direction == Direction.Left)
            return Vector2.left;
        else
            return Vector2.right;
    }

    public static Vector2 GetVerticalDirection(this Vector2 direction)
    {
        if (direction.x != 0f && direction.y != 0) return Vector2Int.zero;

        return direction.x == 0 ? Vector2Int.right : Vector2Int.up;
    }

    public static Vector2Int GetVerticalDirection(this Vector2Int direction)
    {
        return direction.ToVector2().GetVerticalDirection().ToVector2Int();
    }

    public static Vector2Int ToVector2Int(this Vector2 vector)
    {
        return new Vector2Int((int)(vector.x), (int)(vector.y));
    }

    public static Vector2 ToVector2(this Vector2Int vector)
    {
        return new Vector2(vector.x,vector.y);
    }
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