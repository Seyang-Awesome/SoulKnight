using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffCommand
{
    public void OnBuffEnter(BuffInfo info);
    public void OnBuffExit(BuffInfo info);
    public void OnBuffInvoke(BuffInfo info);
}

