using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Exit : MonoBehaviour
{
    public event UnityAction<bool> LevelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            LevelCompleted?.Invoke(true);
        }
    }
}
