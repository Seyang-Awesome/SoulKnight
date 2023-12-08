using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;

namespace WeaponSystem
{
    [CreateAssetMenu(menuName = "Weapon/Laser")]
    public class WeaponDefition_Laser : WeaponDefinitionBase
    {
        [Header("激光模块")] 
        
        public float startTime;
        public float damageInterval;
        public Bullet_Laser laser;
        public Vector2 laserHeadPos;
    }
}


