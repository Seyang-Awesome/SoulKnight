using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class WeaponComponentTypeGetter : MonoSingleton<WeaponComponentTypeGetter>
{
    private Dictionary<int, Type> weaponTypeDic = new();

    private void Start()
    {
        weaponTypeDic = new()
        {
            {11001,typeof(WeaponInGame_OneShoot)},
            {12001,typeof(WeaponInGame_Rifle)},
        };
    }

    public Type GetRelevantType(int i)
    {
        return weaponTypeDic[i];
    }
}