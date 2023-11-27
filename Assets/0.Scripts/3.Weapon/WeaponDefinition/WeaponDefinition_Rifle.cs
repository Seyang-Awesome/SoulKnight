using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Weapon/Rifle")]
    public class WeaponDefinition_Rifle : WeaponDefinitionBase
    {
        [Header("步枪模块")] 
    
        public BulletBase[] bullets;

        public Vector2 bulletExitPos;
        public float shakeIntensity;
        public float shakeTime;
        public float backTime;
    }
}


