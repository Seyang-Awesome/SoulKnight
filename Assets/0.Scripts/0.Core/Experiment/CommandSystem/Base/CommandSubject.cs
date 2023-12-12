using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandSubject
{
    private GameObject relevantGameObject;
    private ICommand[] commands;
    
    public CommandSubject(GameObject gameObject, ICommand[] destroyCommands)
    {
        relevantGameObject = gameObject;
        commands = destroyCommands;
    }
    
    public void InvokeCommands()
    {
        foreach (var command in commands)
        {
            command.OnInvoke(relevantGameObject);
        }
    }
}
