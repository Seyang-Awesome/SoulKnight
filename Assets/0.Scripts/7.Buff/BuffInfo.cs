using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo
{
    public Hurtable Target { get; private set; }
    public float Interval { get; private set; }
    public int Times { get; private set; }
    public float Duration => Interval * Times;
    
    public List<BuffType> BuffTypes = new();

    public BuffInfo(Hurtable target,float interval,int times, int buffIndex)
    {
        Target = target;
        Interval = interval;
        Times = times;

        if ((buffIndex & (int)BuffType.Poison) != 0)
            BuffTypes.Add(BuffType.Poison);
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

