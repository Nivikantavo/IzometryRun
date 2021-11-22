using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float _interactRadius;
    [SerializeField] private Transform _interactionTransform;

    private bool _isFocus = false;
    private bool _hasInteract = false;

    private Transform _playerPosition;
    public float InteractRadius => _interactRadius;
    public Transform InteractionTransform => _interactionTransform;
    

    private void Update()
    {
        if (_isFocus && !_hasInteract)
        {
            float distance = Vector3.Distance(_playerPosition.position, _interactionTransform.position);
            if (distance <= _interactRadius)
            {
                Interact();
                _hasInteract = true;
            }
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Interact with " + transform.name);
    }

    public void OnFocused(Transform player)
    {
        _isFocus = true;
        _playerPosition = player;
        _hasInteract = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _playerPosition = null;
        _hasInteract = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_interactionTransform == null)
            _interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_interactionTransform.position, _interactRadius);
    }
}
