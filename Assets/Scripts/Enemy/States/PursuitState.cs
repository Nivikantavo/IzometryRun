using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(RayCheck))]
[RequireComponent(typeof(NavMeshAgent))]
public class PursuitState : State
{
    private Enemy _enemy;
    private RayCheck _rayCheck;
    private NavMeshAgent _agent;
    private Animator _animator;
    private int _damage;
    private float _speed;
    private float _attackDelay;
    private float _attackDistane;
    private float _lastAttackTime;
    private bool _targetIsVisible;
    private Vector3 _lastTargetPosition;

    public event UnityAction TargetLost;
    public event UnityAction<Vector3> NeedFollowingTarget;

    private const string AttackTrigger = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _rayCheck = GetComponent<RayCheck>();
        _agent = GetComponent<NavMeshAgent>();
        _speed = _enemy.PursuitSpeed;
        _damage = _enemy.Damage;
        _attackDelay = _enemy.AttackDelay;
        _attackDistane = _enemy.AttackDistance;
    }

    private void Update()
    {
        _targetIsVisible = _rayCheck.TargetIsVisible;
        
        if (_targetIsVisible)
        {
            FollowTarget(Target);

            if (Vector3.Distance(transform.position, Target.transform.position) <= _attackDistane + 0.3f) 
            {
                if (_lastAttackTime <= 0)
                {
                    Attack(Target);
                }
            }
            else
            {
                NeedFollowingTarget?.Invoke(Target.transform.position);
            }
            _lastAttackTime -= Time.deltaTime;
        }
        else
        {
            LookAround();
        }
    }

    private void OnEnable()
    {
        _rayCheck.TargetOutSight += TargetOutSight;
        _targetIsVisible = _rayCheck.TargetIsVisible;
        _agent.speed = _speed;
    }

    private void OnDisable()
    {
        _rayCheck.TargetOutSight -= TargetOutSight;
    }

    private void TargetOutSight(Vector3 lastTargetPosition)
    {
        _lastTargetPosition = lastTargetPosition;
    }

    private void FollowTarget(Player target)
    {
        Vector3 offset = (Target.transform.position - transform.position).normalized * _attackDistane;

        _agent.SetDestination(target.transform.position - offset);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, Time.deltaTime * 10);
    }

    private void Attack(Player target)
    {
        _animator.SetTrigger(AttackTrigger);
        target.TakeDamage(_damage);
        _lastAttackTime = _attackDelay;
    }

    private void LookAround()
    {
        _agent.SetDestination(_lastTargetPosition);
        if (transform.position == _lastTargetPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, Time.deltaTime * 100);
            TargetLost?.Invoke();
        }
    }
}
