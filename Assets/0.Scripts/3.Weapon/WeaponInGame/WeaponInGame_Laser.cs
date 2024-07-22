using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class WeaponInGame_Laser : WeaponInGameBase
{
    private WeaponDefition_Laser wd;
    private Bullet_Laser laser;
    private Bullet_Laser currentLaser;

    private float startTime;
    private float startCounter;
    private float damageInterval;
    private float damageCounter;
    
    public override void Init(WeaponDefinitionBase weaponDefinitionBase)
    {
        base.Init(weaponDefinitionBase);
        
        wd = weaponDefinitionBase as WeaponDefition_Laser;
        laser = wd.laser;

        startTime = wd.startTime;
        damageInterval = wd.damageInterval;
        ResetCounter();
        
        bulletExitPosTransform.localPosition = wd.laserHeadPos;
    }

    public override void Attack(Vector2 direction)
    {
        Shake();

        if (currentLaser == null)
        {
            currentLaser = PoolManager.Instance.GetGameObject(laser);
            currentLaser.SetLaserWidth(1f);
            currentLaser.Init(new BulletInfo(wd,Vector2.zero));
        }
        
        //启动时间
        Vector2 position = bulletExitPosTransform.transform.position;
        
        startCounter -= Time.deltaTime;
        if (startCounter >= 0)
        {
            currentLaser.SetLaser(position,Vector2.zero);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction,
                Consts.LaserMaxDistance, Consts.MapLayerMask);
            currentLaser.SetLaser(position,hit.point - position);
        }

        damageCounter -= Time.deltaTime;
        if (damageCounter <= 0f)
        {
            damageCounter = damageInterval;
            currentLaser.Hurt();
        }
    }

    public override void StopAttack()
    {
        if(currentLaser == null) return;
        PoolManager.Instance.PushGameObject(currentLaser.gameObject);
        currentLaser = null;
        ResetCounter();
    }

    protected override void Shake()
    {

    }

    private void ResetCounter()
    {
        startCounter = startTime;
        damageCounter = damageInterval;
    }
    

}

