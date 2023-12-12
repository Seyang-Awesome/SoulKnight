using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : Hurtable
{
    [SerializeField] protected ParticleCommand particleCommand;

    private int currentHealth;
    protected CommandSubject commandSubject;
    private void OnEnable()
    {
        currentHealth = 5;
    }

    protected virtual void Awake()
    {
        commandSubject = new CommandSubject(gameObject, new ICommand[]
        {
            particleCommand,
            new PushToPoolCommand(),
        });
    }

    public override void Hurt(HurtInfo info)
    {
        currentHealth -= info.Damage;
        if (currentHealth <= 0)
            commandSubject.InvokeCommands();
    }
    
}

