using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ListenerCommponent : MonoBehaviour, ICommand
{
    public abstract void OnInvoke(GameObject gameObject);
}


