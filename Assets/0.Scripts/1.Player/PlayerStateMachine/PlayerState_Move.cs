using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Move : PlayerState
{
    public override void Enter()
    {
        controller.PlayAnimation(AniamtionType.Move);
    }

    public override void PhysicsUpdate()
    {
        controller.SetVelocity(input.moveInput * info.moveSpeed);
    }

    public override void LogicUpdate()
    {
        if (info.isIdling)
        {
            stateMachine.SwitchState<PlayerState_Idle>();
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
