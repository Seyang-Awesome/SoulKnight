using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace WeaponSystem
{
    public class WeaponInGame_Rifle : WeaponInGameBase
    {
        private new WeaponDefinition_Rifle wd;
        private BulletBase bullet;
        public override void Init(WeaponDefinitionBase weaponDefinitionBase)
        {
            base.Init(weaponDefinitionBase);
        
            wd = weaponDefinitionBase as WeaponDefinition_Rifle;
            bullet = wd.bullets[0];
            bulletExitPosTransform.localPosition = wd.bulletExitPos;
        }

        public override void Attack(Vector2 direction)
        {
            if (!isCanAttack) return;
        
            base.Attack(direction);
            Bullet_Single newBullet = PoolManager.Instance.GetGameObject(bullet) as Bullet_Single;
            newBullet.Init(new BulletInfo(wd,direction));
            newBullet.transform.position = bulletExitPosTransform.transform.position;
        }

        protected override async UniTask Shake()
        {
            isShaking = true;

            Vector3 startAngle = new Vector3();
            Vector3 aimAngle = new Vector3(0, 0, 5 * wd.shakeIntensity);
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
}

