using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponentTypeGetter : MonoSingleton<WeaponComponentTypeGetter>
{
    private Dictionary<int, Type> weaponTypeDic = new();

    private void Start()
    {
        weaponTypeDic = new()
        {
            {1001,typeof(WeaponInGame_OneShoot)},
        };
    }

    public Type GetRelevantType(int i)
    {
        return weaponTypeDic[i];
    }
}