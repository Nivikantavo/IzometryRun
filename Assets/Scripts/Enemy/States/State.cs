using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if(enabled == false)
        {
            Target = target;

            enabled = true;
            foreach(var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(target);
            }
        }
    }

    public void Exit()
    {
        if(enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach(var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }
        return null;
    }
}
