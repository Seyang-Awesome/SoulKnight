using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using WeaponSystem;

[CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Weapon/Knife")]
public class WeaponDefinition_Knife : WeaponDefinitionBase
{
    [Header("近战武器模块")] 
    
    public Vector2 attackBox;

    public float spriteSpeed;
    public float effectSpeed;
    public float damageIntensity;
    
    public RuntimeAnimatorController weaponSpriteAnimator;
    public RuntimeAnimatorController weaponEffectAnimator;
}

