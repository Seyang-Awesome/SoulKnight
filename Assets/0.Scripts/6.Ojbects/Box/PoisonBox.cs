using System;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBox : Box
{
    [SerializeField] protected SetGameObjectCommand setGameObjectCommand;
    protected override void Awake()
    {
        commandSubject = new CommandSubject(gameObject, new ICommand[]
        {
            particleCommand,
            setGameObjectCommand,
            new PushToPoolCommand(),
        });
    }
}

