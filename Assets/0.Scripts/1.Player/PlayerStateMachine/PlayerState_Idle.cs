using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        controller.PlayAnimation(AnimationType.Idle);
        controller.SetVelocity(Vector2.zero);
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (info.hurt)
        {
            stateMachine.SwitchState<PlayerState_Hurt>();
            return;
        }
        
        if (info.MoveAction)
        {
            stateMachine.SwitchState<PlayerState_Move>();
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
