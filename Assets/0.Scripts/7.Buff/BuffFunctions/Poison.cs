using System;
using System.Collections.Generic;
using UnityEngine;

public class Poison : IBuffCommand
{
    public void OnBuffEnter(BuffInfo info)
    {
        Debug.Log("OnBuffEnter");
    }

    public void OnBuffExit(BuffInfo info)
    {
        Debug.Log("OnBuffExit");
    }

    public void OnBuffInvoke(BuffInfo info)
    {
        Debug.Log("OnBuffInvoke");
    }
}

