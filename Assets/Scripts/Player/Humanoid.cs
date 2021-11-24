using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


[RequireComponent(typeof(NavMeshAgent))]
public abstract class Humanoid : MonoBehaviour
{
    [SerializeField] protected int _health;
    protected int _currentHealth;

    private bool _isDead = false;
    private NavMeshAgent _agent;

    public int CurrentHealth => _currentHealth;

    public event UnityAction<bool> HumanDie;

    public virtual void DisableMovment()
    {
        _agent.speed = 0;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead == false)
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
        }

        if (_currentHealth <= 0 && _isDead == false)
        {
            Die();
        }
    }

    protected void Start()
    {
        _currentHealth = _health;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Die()
    {
        _isDead = true;
        HumanDie?.Invoke(false);
    }

    public void RestoreHealth(int HeathUnits)
    {
        if (_currentHealth < _health)
            _currentHealth += HeathUnits;

        if (_currentHealth > _health)
        {
            _currentHealth = _health;
        }
    }
}
