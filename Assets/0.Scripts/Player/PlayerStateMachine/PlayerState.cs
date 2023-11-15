using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected GameInput input;
    protected PlayerStateMchine stateMachine;
    protected PlayerController controller;
    public abstract void Enter();

    public abstract void PhysicsUpdate();

    public abstract void LogicUpdate();

    public abstract void Exit();

    public void Init(PlayerStateMchine stateMachine,PlayerController controller)
    {
        this.stateMachine = stateMachine;
        this.controller = controller;
        input = GameInput.instance;
    }
}
