using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMchine : StateMachine
{
    private void Start()
    {
        stateDic = new()
        {
            { typeof(PlayerState_Idle), new PlayerState_Idle() },
            { typeof(PlayerState_Move), new PlayerState_Move() }
        };

        PlayerController controller = GetComponent<PlayerController>();
        
        foreach (var state in stateDic)
        {
            (state.Value as PlayerState).Init(this,controller);
        }
        
        Begin<PlayerState_Idle>();
    }
}
