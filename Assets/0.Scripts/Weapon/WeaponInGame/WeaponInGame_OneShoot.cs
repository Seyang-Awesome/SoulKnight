using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WeaponSystem;
using DG.Tweening;

public class WeaponInGame_OneShoot : WeaponInGameBase
{
    private new WeaponDefinition_OneShoot wd;
    private BulletBase bullet;
    public override void Init(WeaponDefinitionBase weaponDefinitionBase)
    {
        base.Init(weaponDefinitionBase);
        
        wd = weaponDefinitionBase as WeaponDefinition_OneShoot;
        bullet = wd.bullets[0];
    }

    public override void Attack()
    {
        base.Attack();
        // BulletBase newBullet = PoolManager.instance.GetGameObject(bullet);
        
    }

    protected override async UniTask Shake()
    {
        isShaking = true;

        Vector3 startAngle = new Vector3();
        Vector3 aimAngle = new Vector3(0, 0, 15 * wd.shakeIntensity);
        Vector3 startPos = new Vector3();
        Vector3 aimPos = new Vector3(-wd.shakeIntensity / 10, 0, 0);
        
        weaponSpriteTransform.DOLocalRotate(aimAngle, wd.shakeTime);
        weaponSpriteTransform.DOLocalMove(aimPos, wd.shakeTime);

        await UniTask.Delay(TimeSpan.FromSeconds(wd.shakeTime));

        weaponSpriteTransform.DOLocalRotate(startAngle, wd.backTime);
        weaponSpriteTransform.DOLocalMove(startPos, wd.backTime);

        await UniTask.Delay(TimeSpan.FromSeconds(wd.backTime));

        isShaking = false;
    }
}