using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo
{
    public Hurtable Target { get; private set; }
    public int BuffIndex { get; private set; }
    public float Interval { get; private set; }
    public int Times { get; private set; }

    public BuffInfo(Hurtable target,float interval,int times, int buffIndex)
    {
        Target = target;
        BuffIndex = buffIndex;
        Interval = interval;
        Times = times;
    }
    
    // public void AddBuffType(BuffType buffType)
    // {
    //     if ((BuffIndex & (int)buffType) != 0)
    //         return;
    //     BuffIndex |= (int)buffType;
    // }
    //
    // public void RemoveBuffType(BuffType buffType)
    // {
    //     if ((BuffIndex & (int)buffType) == 0)
    //         return;
    //     BuffIndex &= (int)buffType;
    // }
}

