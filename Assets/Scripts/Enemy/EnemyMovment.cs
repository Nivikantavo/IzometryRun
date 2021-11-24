using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(StopState))]
[RequireComponent(typeof(PursuitState))]
[RequireComponent(typeof(PatrolState))]
[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovment : MonoBehaviour
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Player _target;
    private ThirdPersonCharacter _character;
    private PursuitState _pursuitState;
    private PatrolState _patrolState;
    private StopState _stopState;
    private Vector3 _targetPoint;
    private bool _needMove = false;

    private void Awake()
    {
        _stopState = GetComponent<StopState>();
        _pursuitState = GetComponent<PursuitState>();
        _patrolState = GetComponent<PatrolState>();
        _character = GetComponent<ThirdPersonCharacter>();
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_needMove)
        {
            if (_target != null)
            {
                FollowTarget(_target);
            }
            else
            {
                StopFollowingTarget();
                MoveToPoint(_targetPoint);
            }

            _needMove = false;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            _character.Move(_agent.desiredVelocity, false, false);
        }
        else
        {
            _character.Move(Vector3.zero, false, false);
        }
    }

    private void OnEnable()
    {
        _stopState.NeedStop += NeedMove;
        _pursuitState.NeedFollowingTarget += NeedMove;
        _patrolState.NeedMove += NeedMove;
    }

    private void OnDisable()
    {
        _stopState.NeedStop -= NeedMove;
        _pursuitState.NeedFollowingTarget -= NeedMove;
        _patrolState.NeedMove -= NeedMove;
    }

    private void FollowTarget(Player target)
    {
        _agent.stoppingDistance = _enemy.AttackDistance;
        _agent.SetDestination(target.transform.position);
    }

    private void StopFollowingTarget()
    {
        _agent.stoppingDistance = 0.2f;
    }

    private void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void SetTarget(Player target)
    {
        _target = target;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    private void NeedMove(Vector3 point)
    {
        _targetPoint = point;
        _needMove = true;
    }
}
