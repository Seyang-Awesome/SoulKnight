using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Hurt : PlayerState
{
    private float counter;
    public override void Enter()
    {
        counter = Consts.BackTime;
        controller.SetVelocity(info.hurtInfo.DamageDirection * Consts.BackVelocity);
        
        info.hurt = false;
        info.hurtInfo = null;
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void LogicUpdate()
    {
        counter -= Time.deltaTime;
        if (counter >= 0) return;
        
        if (info.MoveAction)
        {
            stateMachine.SwitchState<PlayerState_Move>();
            return;
        }

        if (info.IsIdling)
        {
            stateMachine.SwitchState<PlayerState_Idle>();
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
