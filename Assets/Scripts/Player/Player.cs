using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMovment))]
public class Player : Humanoid
{
    private Camera _camera;
    private Interactable _focus;
    private PlayerMovment _playerMovment;

    private Vector3 _targetPoint;

    public event UnityAction<Vector3> OnMoveButtonClick;

    public override void DisableMovment()
    {
        base.DisableMovment();
    }

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _playerMovment = GetComponent<PlayerMovment>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    SetFocus(interactable);
                    _targetPoint = interactable.transform.position;
                }
                else
                {
                    RemoveFocus();
                    _targetPoint = hit.point;
                }
            }

            OnMoveButtonClick?.Invoke(_targetPoint);
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if(newFocus != _focus)
        {
            if (_focus != null)
                _focus.OnDefocused();

            _focus = newFocus;
        }

        _playerMovment.SetTarget(_focus);
        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if(_focus != null)
            _focus.OnDefocused();

        _playerMovment.RemoveTarget();
        _focus = null;
    }

    protected override void Die()
    {
        base.Die();
    }
}
