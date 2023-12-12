using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public void OnInvoke(GameObject gameObject);
}

