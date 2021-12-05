using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Door : MonoBehaviour
{
    [SerializeField] private LevelButton _doorButton;
    [SerializeField] private bool _isOpen;

    private NavMeshObstacle _obstacle;
    private Animator _animator;
    private const string _open = "Open";

    public event UnityAction DoorOpened;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _obstacle = GetComponent<NavMeshObstacle>();

        _animator.SetBool(_open, _isOpen);
    }

    private void OnEnable()
    {
        _doorButton.ButtonClicked += OnDoorButtonClick;
    }

    private void OnDisable()
    {
        _doorButton.ButtonClicked -= OnDoorButtonClick;
    }
    private void OnDoorButtonClick()
    {
        _isOpen = !_isOpen;
        _animator.SetBool(_open, _isOpen);
    }

    public void OpenDoor()
    {
        _obstacle.enabled = false;
        DoorOpened?.Invoke();
    }
}
