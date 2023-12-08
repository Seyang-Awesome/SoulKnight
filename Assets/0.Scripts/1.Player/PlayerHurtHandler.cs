using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtHandler : Hurtable
{
    private PlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            BuffManager.Instance.AddBuff(new BuffInfo(this,1,5,(int)BuffType.Poison));
    }

    public override void Hurt(HurtInfo hurtInfo)
    {
        playerInfo.Hurt(hurtInfo);
        Flash();
    }
    
}

