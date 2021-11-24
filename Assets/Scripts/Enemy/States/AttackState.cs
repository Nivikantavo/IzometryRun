using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class AttackState : State
{
    private int _damage;
    private float _attackDelay;
    private float _lastAttackTime;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _damage = _enemy.Damage;
        _attackDelay = _enemy.AttackDelay;
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _attackDelay;
        }
        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        target.TakeDamage(_damage);
    }
}
