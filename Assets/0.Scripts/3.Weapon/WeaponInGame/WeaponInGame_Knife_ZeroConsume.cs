using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using WeaponSystem;

public class WeaponInGame_Knife_ZeroConsume : WeaponInGame_Knife
{
    private WeaponDefinition_Knife wd;
    private Vector2 attackDirection;
    private Vector2 AttackCenter => transform.position + (Vector3)attackDirection * wd.attackBox.x / 2;
    public override void Init(WeaponDefinitionBase weaponDefinitionBase)
    {
        base.Init(weaponDefinitionBase);
        
        wd = weaponDefinitionBase as WeaponDefinition_Knife;
        weaponSpriteAnimator.runtimeAnimatorController = wd.weaponSpriteAnimator;
        weaponEffectAnimator.runtimeAnimatorController = wd.weaponEffectAnimator;
        weaponSpriteAnimator.speed = wd.spriteSpeed;
        weaponEffectAnimator.speed = wd.effectSpeed;
        weaponEffectTransform.AddComponent<KnifeAttackTiming>().Init();
    }
    
    public override void Attack(Vector2 direction)
    {
        if (!IsCanAttack) return;
        
        base.Attack(direction);
        attackDirection = direction;
    }

    public override void OnAttackTiming()
    {
        Debug.Log("OnAttackTiming");
        Collider2D[] detecteds = Physics2D.OverlapBoxAll(AttackCenter, wd.attackBox,
            Vector2.Angle(Vector2.right, attackDirection), Consts.PlayerTargetLayerMask);
        // detecteds.ToList().ForEach(detect => Debug.Log(detect));
        // Debug.Log(attackDirection);
        HurtInfo hurtInfo = new HurtInfo(wd.damage, attackDirection);
        detecteds?.ToList().ForEach(detect => detect.GetComponent<Hurtable>().Hurt(hurtInfo));
    }
    
    protected override void Shake()
    {
        //计算各种参数
        int waveIndex = ComboIndex % 2;
        Vector3 currentSpriteScale = weaponSpriteTransform.localScale;
        Vector3 currentEffectScale = weaponEffectTransform.localScale;
        
        weaponSpriteTransform.localScale = waveIndex == 0
            ? new Vector3(currentSpriteScale.x, Mathf.Abs(currentSpriteScale.y), 1)
            : new Vector3(currentSpriteScale.x, -Mathf.Abs(currentSpriteScale.y), 1);
        weaponEffectTransform.localScale = waveIndex == 0
            ? new Vector3(currentEffectScale.x, Mathf.Abs(currentEffectScale.y), 1)
            : new Vector3(currentEffectScale.x, -Mathf.Abs(currentEffectScale.y), 1);
        
        if(waveIndex == 0)
            weaponSpriteAnimator.Play("WaveUp");
        else
            weaponSpriteAnimator.Play("WaveDown");

        weaponEffectAnimator.Play("Light");
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(AttackCenter, wd.attackBox);
    }
#endif
}



// //计算各种参数
// int waveIndex = ComboIndex % 2;
//
// Vector3 startAngle = Vector3.zero;
//
// Vector3 initAngle = new Vector3(0,0,wd.maxAngle * waveIndex);
// while (initAngle.z < 0)
//     initAngle += new Vector3(0, 0, 360);
// while (initAngle.z > 0)
//     initAngle += new Vector3(0, 0, -360);
// Vector3 aimAngle1 = initAngle + new Vector3(0,0,wd.maxAngle * waveIndex);
// Vector3 aimAngle2 = initAngle + new Vector3(0, 0, wd.maxAngle * 0.25f * waveIndex);
//
// Sprite[] effects = new Sprite[wd.knifeLightAtlas.spriteCount] ;
// wd.knifeLightAtlas.GetSprites(effects);
//
// //先在一帧内运动到最大位置
// weaponSpriteTransform.localEulerAngles = initAngle;
// UniTask.Delay(TimeSpan.FromSeconds(0.06f));
//
// //再挥动到最大位置
// weaponSpriteTransform.DOLocalRotate(aimAngle1,wd.waveTime, RotateMode.FastBeyond360);
// UniTask.Delay(TimeSpan.FromSeconds(wd.waveTime / 3 * 2));
// weaponSr.sprite = effects[0];
// UniTask.Delay(TimeSpan.FromSeconds(wd.waveTime / 3));
// weaponEffectSr.sprite = effects[1];
// UniTask.Delay(TimeSpan.FromSeconds(wd.waveTime / 3));
// weaponEffectSr.sprite = effects[2];
//
// //再挥动到恢复偏移位置
// weaponSpriteTransform.DOLocalRotate(aimAngle2, wd.backTime);
// weaponEffectSr.sprite = null;
// UniTask.Delay(TimeSpan.FromSeconds(wd.backTime));
//
// //再挥动到初始位置
// weaponSpriteTransform.DOLocalRotate(startAngle,wd.adjustTime);
// UniTask.Delay(TimeSpan.FromSeconds(wd.adjustTime));

