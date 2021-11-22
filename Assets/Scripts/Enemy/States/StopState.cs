using UnityEngine;
using UnityEngine.Events;

public class StopState : State
{
    private Enemy _enemy;

    public event UnityAction<Vector3> NeedStop;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        NeedStop?.Invoke(transform.position);
        _enemy.DisableMovment();
    }
}
