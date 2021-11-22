using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private LevelButton _doorButton;
    [SerializeField] private bool _isOpen;
    private NavMeshObstacle _obstacle;
    private Animator _animator;

    public event UnityAction DoorOpened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _obstacle = GetComponent<NavMeshObstacle>();

        _animator.SetBool("Open", _isOpen);
    }

    private void OnEnable()
    {
        _doorButton.ButtonClick += OnDoorButtonClick;
    }

    private void OnDisable()
    {
        _doorButton.ButtonClick -= OnDoorButtonClick;
    }
    private void OnDoorButtonClick()
    {
        _isOpen = !_isOpen;
        _animator.SetBool("Open", _isOpen);
    }

    public void OpenDoor()
    {
        _obstacle.enabled = false;
        DoorOpened?.Invoke();
    }
}
