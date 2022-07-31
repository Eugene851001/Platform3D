using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoSpamAction
{
    int lastUpdate;
    Action _action;
    int _deltaTime;

    public NoSpamAction(int deltaTime, Action action)
    {
        _action = action;
        _deltaTime = deltaTime;
    }

    public void Run()
    {
        if ((Environment.TickCount - lastUpdate) > _deltaTime)
        {
            _action();
            lastUpdate = Environment.TickCount;
        }
    }
}
