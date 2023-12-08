using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffManager : MonoSingleton<BuffManager>
{
    [SerializeField] private BuffIcon buffIconPrefab;
    protected override bool IsDontDestroyOnLoad => false;
    private Dictionary<Hurtable, List<BuffType>> targetEntities = new();
    private Dictionary<Hurtable, BuffIcon> targetIcons = new();
    public void AddBuff(BuffInfo info)
    {
        if(info == null) return;
        Hurtable target = info.Target;
        List<BuffType> relevantBuffList = targetEntities[target];
        
        //检查是否这个对象还处于BUFF效果中，如果没有则添加一个
        if (!targetEntities.ContainsKey(target))
        {
            targetEntities.Add(target, new List<BuffType>());
            targetIcons.Add(target,PoolManager.Instance.GetGameObject<BuffIcon>(buffIconPrefab));
        }
        
        //不能重复受到同一种BUFF的效果
        List<BuffType> buffTypes = new();
        List<IBuffCommand> buffCommands = new();
        info.BuffTypes.ForEach(buffType =>
        {
            if (!relevantBuffList.Contains(buffType))
            {
                buffTypes.Add(buffType);
                buffCommands.Add(GetBuffCommand(buffType));
            }
        });
        
        targetEntities[target] = relevantBuffList.Concat(buffTypes).ToList();
        buffTypes.ForEach(buffType => targetIcons[target].AddIcon(buffType));
        
        //在BuffManager内先执行一次Buff进入方法
        buffCommands.ForEach(buffCommand => buffCommand.OnBuffEnter(info));
        
        //在ScheduleManager内注册Buff持续时的方法
        ScheduleManager.Instance.AddSchedule(new ContinuousSchedule(info.Interval, info.Times,
            () => buffCommands.ForEach(buffCommand => buffCommand.OnBuffInvoke(info))));
        
        //在ScheduleManager内注册Buff结束时的方法
        ScheduleManager.Instance.AddSchedule(new Schedule(info.Duration, () =>
        {
            buffCommands.ForEach(buffCommand => buffCommand.OnBuffExit(info));
            buffTypes.ForEach(buffType =>
            {
                targetEntities[target].Remove(buffType);
                targetIcons[target].RemoveIcon(buffType);
            });
            if (targetEntities[target].Count <= 0)
            {
                targetEntities.Remove(target);
                PoolManager.Instance.PushGameObject(targetIcons[target]);
                targetIcons.Remove(target);
            }
        }));
    }

    // private void AddBuffIcon(Hurtable info, BuffType buffType)
    // {
    //     if (!targetIcons.ContainsKey(info)) return;
    //     BuffIcon buffIcon = targetIcons[info];
    //     buffIcon.AddIcon(buffType);
    // }
    //
    // private void RemoveBuffIcon(Hurtable info, BuffType buffType)
    // {
    //     if (!targetIcons.ContainsKey(info)) return;
    //     BuffIcon buffIcon = targetIcons[info];
    //     buffIcon.RemoveIcon(buffType);
    // }

    private IBuffCommand GetBuffCommand(BuffType buffType)
    {
        if (buffType == BuffType.Poison)
            return new Poison();
        return null;
    }
}
