using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_21002 : EnemyController_ArmedWeapon
{
    private Bullet_Single bullet;
    private void Start()
    {
        bullet = bullets[0] as Bullet_Single;
    }

    public override async void Attack()
    {
        AimAtTarget();

        for (int i = 0; i < 2; i++)
        {
            Bullet_Single newBullet = PoolManager.Instance.GetGameObject(bullet);
            newBullet.Init(new BulletInfo(attackPower, AimAtDirection, 0));
            newBullet.transform.position = BulletExitPos;
            await ShakeWeapon();
        }
    }
}

