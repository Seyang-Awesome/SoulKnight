using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : PlayerState
{
    public override void Enter()
    {
        controller.PlayAnimation(AnimationType.Move);
    }

    public override void PhysicsUpdate()
    {
        controller.SetVelocity(input.moveInput * info.moveSpeed);
    }

    public override void LogicUpdate()
    {
        if (info.hurt)
        {
            stateMachine.SwitchState<PlayerState_Hurt>();
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
