using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtHandler : Hurtable
{
    private PlayerInfo playerInfo;

    protected override void Start()
    {
        base.Start();
        playerInfo = EntityInfo as PlayerInfo;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            BuffManager.Instance.AddBuff(new BuffInfo(this,1,5,(int)BuffType.Poison));
        if(Input.GetKeyDown(KeyCode.Alpha2))
            BuffManager.Instance.AddBuff(new BuffInfo(this,1,5,(int)BuffType.Fire));
    }

    public override void Hurt(HurtInfo hurtInfo)
    {
        playerInfo.Hurt(hurtInfo);
        Flash();
    }
    
}

