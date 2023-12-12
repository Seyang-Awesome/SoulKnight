using System;
using System.Collections.Generic;
using UnityEngine;

public class PushToPoolCommand : ICommand
{
    public void OnInvoke(GameObject gameObject)
    {
        PoolManager.Instance.PushGameObject(gameObject);
    }
}

