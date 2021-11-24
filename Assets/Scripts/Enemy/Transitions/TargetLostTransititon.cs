using UnityEngine;

[RequireComponent(typeof(PursuitState))]
public class TargetLostTransititon : Transition
{
    private PursuitState _pursuitState;

    private void Awake()
    {
        _pursuitState = GetComponent<PursuitState>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _pursuitState.TargetLost += TargetLost;
    }

    private void OnDisable()
    {
        _pursuitState.TargetLost -= TargetLost;
    }

    private void TargetLost()
    {
        NeedTransit = true;
    }
}
