using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[RequireComponent(typeof(NavMeshAgent))]
public abstract class Humanoid : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;

    protected int _currentHealth;

    private NavMeshAgent _agent;

    public int CurrentHealth => _currentHealth;

    public event UnityAction<bool> HumanDied;

    public virtual void DisableMovment()
    {
        _agent.speed = 0;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Start()
    {
        _currentHealth = _maxHealth;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Die()
    {
        HumanDied?.Invoke(false);
        enabled = false;
    }

    public void RestoreHealth(int HeathUnits)
    {
        if (_currentHealth < _maxHealth)
            _currentHealth += HeathUnits;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
