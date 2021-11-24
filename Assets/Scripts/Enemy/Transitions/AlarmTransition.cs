using UnityEngine;

[RequireComponent(typeof(RayCheck))]
public class AlarmTransition : Transition
{
    private RayCheck _rayCheck;

    private void Awake()
    {
        _rayCheck = GetComponent<RayCheck>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _rayCheck.Alarm += Alarm;
    }

    private void OnDisable()
    {
        _rayCheck.Alarm -= Alarm;
    }

    private void Alarm()
    {
        NeedTransit = true;
    }
}
