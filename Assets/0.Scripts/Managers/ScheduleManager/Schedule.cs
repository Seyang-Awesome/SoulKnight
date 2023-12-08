using System;
using UnityEngine;

public class Schedule
{
    public float Duration { get; private set; }
    public float StartTime { get; private set; }
    public float EndTime { get; private set; }
    
    private event Action OnStart;
    private event Action OnEnd;
    
    public Schedule(float duration)
    {
        this.Duration = duration;
        StartTime = Time.realtimeSinceStartup;
        EndTime = Time.realtimeSinceStartup + duration;
    }
    public Schedule(float duration,Action onEndCallback)
    {
        this.Duration = duration;
        StartTime = Time.realtimeSinceStartup;
        EndTime = Time.realtimeSinceStartup + duration;
        
        this.OnStart = null;
        this.OnEnd = onEndCallback;
    }
    public Schedule(float duration,Action onStartCallback,Action onEndCallback)
    {
        this.Duration = duration;
        StartTime = Time.realtimeSinceStartup;
        EndTime = Time.realtimeSinceStartup + duration;
        
        this.OnStart = onStartCallback;
        this.OnEnd = onEndCallback;
    }

    public void InvokeStartCallback()
    {
        OnStart?.Invoke();
    }

    public void InvokeEndCallback()
    {
        OnEnd?.Invoke();
    }
    
}