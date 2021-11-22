using UnityEngine;

public class DieTransition : Transition
{
    [SerializeField] private Enemy _enemy;
    private void Update()
    {
        if (_enemy.CurrentHealth <= 0)
            NeedTransit = true;
    }
}
