using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected GameInput input;
    protected PlayerInfo info;
    protected PlayerStateMchine stateMachine;
    protected PlayerController controller;
    public abstract void Enter();

    public abstract void PhysicsUpdate();

    public abstract void LogicUpdate();

    public abstract void Exit();

    public void Init(PlayerInfo info, PlayerStateMchine stateMachine,PlayerController controller)
    {
        this.info = info;
        this.stateMachine = stateMachine;
        this.controller = controller;
        input = GameInput.Instance;
    }
}
