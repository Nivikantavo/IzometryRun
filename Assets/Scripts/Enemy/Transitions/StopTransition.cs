using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTransition : Transition
{
    private Exit _exit;

    private void Awake()
    {
        _exit = FindObjectOfType<Exit>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _exit.OnLevelCompleted += NeedStop;
    }

    private void OnDisable()
    {
        _exit.OnLevelCompleted -= NeedStop;
    }

    private void NeedStop(bool complited)
    {
        NeedTransit = complited;
    }
}
