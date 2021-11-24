using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Exit : MonoBehaviour
{
    public event UnityAction<bool> OnLevelCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            OnLevelCompleted?.Invoke(true);
        }
    }
}
