using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoSingleton<BuffManager>
{
    protected override bool IsDontDestroyOnLoad => false;

    public void AddBuff(BuffInfo info)
    {
        List<IBuffCommand> buffCommands = new();
        if((info.BuffIndex & (int)BuffType.Poison) != 0)
            buffCommands.Add(new Poison());
        
        buffCommands.Count.Log();
        
        //在BuffManager内先执行一次Buff进入方法
        buffCommands.ForEach(buffCommand => buffCommand.OnBuffEnter(info));
        //在ScheduleManager内注册Buff持续时的方法
        void InvokeBuff() => buffCommands.ForEach(buffCommand => buffCommand.OnBuffInvoke(info));
        ScheduleManager.Instance.AddSchedule(new ContinuousSchedule(info.Interval, info.Times,InvokeBuff));
        //在ScheduleManager内注册Buff结束时的方法
        void OnBuffEnd() => buffCommands.ForEach(buffCommand => buffCommand.OnBuffExit(info));
        ScheduleManager.Instance.AddSchedule(new Schedule(info.Interval * info.Times,OnBuffEnd));
    }
}

