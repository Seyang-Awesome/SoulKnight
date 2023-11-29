using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class EnemyController_ArmedWeapon : EnemyController
{
    [Header("EnemyController_ArmedWeapon")]
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    [SerializeField] private float backTime;
    [SerializeField] protected int attackPower;
    [SerializeField] protected BulletBase[] bullets;

    private Transform weaponRootTransform;
    private Transform weaponTransform;
    private Transform weaponBulletExitTransform;

    protected Vector2 AimAtDirection => weaponTransform.right * scaleIndex;
    protected Vector2 BulletExitPos => weaponBulletExitTransform.transform.position;

    protected override void Awake()
    {
        base.Awake();
        weaponRootTransform = transform.GetChild(Consts.EnemyWeaponIndex);
        weaponTransform = weaponRootTransform.GetChild(0);
        weaponBulletExitTransform = weaponTransform.GetChild(0);
    }

    public override void SetFaceDirection()
    {
        // base.SetFaceDirection();
        if(info.target == null)
            base.SetFaceDirection();
        else
        {
            if (info.TargetDirection.x > 0.1f)
                transform.localScale = enemyScale;
            else
                transform.localScale = inverseEnemyScale;
            weaponRootTransform.right = info.TargetDirection * scaleIndex;
        }
    }

    public void AimAtTarget()
    {
        if (info.target == null) return;
        weaponTransform.right = info.TargetDirection * scaleIndex;
    }

    public void ResetAim()
    {
        weaponTransform.right = Vector2.right * scaleIndex;
    }

    public async UniTask ShakeWeapon()
    {
        Vector3 startAngle = new Vector3();
        Vector3 aimAngle = new Vector3(0, 0, 15 * shakeIntensity);
        Vector3 startPos = new Vector3();
        Vector3 aimPos = new Vector3(-shakeIntensity / 10, 0, 0);
        
        weaponTransform.DOLocalRotate(aimAngle, shakeTime);
        weaponTransform.DOLocalMove(aimPos, shakeTime);

        await UniTask.Delay(TimeSpan.FromSeconds(shakeTime));

        weaponTransform.DOLocalRotate(startAngle, backTime);
        weaponTransform.DOLocalMove(startPos, backTime);

        await UniTask.Delay(TimeSpan.FromSeconds(backTime));
    }

    public override void Attack()
    {
        base.Attack();
    }
}

