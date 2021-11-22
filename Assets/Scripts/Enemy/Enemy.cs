using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Humanoid
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _pursuitSpeed;
    [SerializeField] private float _standartSpeed;
    [SerializeField] private float _attackDistance;

    private Player _target;
    private EnemyStateMachine _enemyStateMachine;
    private RayCheck _rayCheck;

    public Player Target => _target;
    public float StandartSpeed => _standartSpeed;
    public float PursuitSpeed => _pursuitSpeed;
    public float AttackDelay => _attackDelay;
    public int Damage => _damage;
    public float AttackDistance => _attackDistance;
    private void Awake()
    {
        _target = FindObjectOfType<Player>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _rayCheck = GetComponent<RayCheck>();
    }

    protected override void Die()
    {
        base.Die();
    }

    public override void DisableMovment()
    {
        base.DisableMovment();

        _rayCheck.enabled = false;
        _enemyStateMachine.Curent.enabled = false;
        _enemyStateMachine.enabled = false;
    }


}
