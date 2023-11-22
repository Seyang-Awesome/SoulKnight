using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        controller.PlayAnimation(PlayerAniamtionType.Idle);
    }

    public override void PhysicsUpdate()
    {
        controller.SetVelocity(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        if (info.moveAction)
        {
            stateMachine.SwitchState<PlayerState_Move>();
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
