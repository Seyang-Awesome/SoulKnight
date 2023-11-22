using System;
using UnityEngine;

public class GameInput : MonoSingleton<GameInput>
{
    private PlayerInputActions input;
    public Vector2 moveInput => input.GamePlay.Move.ReadValue<Vector2>();
    public bool attackInput => input.GamePlay.Attack.WasPressedThisFrame();
    private void Start()
    {
        input = new PlayerInputActions();
        input.Enable();
    }
    
}