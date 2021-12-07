using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovment : MonoBehaviour
{
    private Player _player;
    private NavMeshAgent _agent;
    private Interactable _target;
    private ThirdPersonCharacter _character;
    private Vector3 _targetPoint;
    private bool _needMove = false;

    private void Awake()
    {
        _character = GetComponent<ThirdPersonCharacter>();
        _player = GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateRotation = false;
    }

    private void Update()
    {
        if (_needMove)
        {
            if (_target != null)
            {
                FollowTarget(_target);
            }
            else
            {
                StopFollowingTarget();
                MoveToPoint(_targetPoint);
            }

            _needMove = false;
        }

        if (_agent.remainingDistance > _agent.stoppingDistance)
        {
            _character.Move(_agent.desiredVelocity, false, false);
        }
        else
        {
            _character.Move(Vector3.zero, false, false);
        }
    }

    private void OnEnable()
    {
        _player.OnMoveButtonClick += NeedMove;
    }

    private void OnDisable()
    {
        _player.OnMoveButtonClick -= NeedMove;
    }

    private void FollowTarget(Interactable target)
    {
        _agent.stoppingDistance = target.InteractRadius;
        _agent.SetDestination(target.InteractionTransform.position);
    }

    private void StopFollowingTarget()
    {
        _agent.stoppingDistance = 0.2f;
    }

    private void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void SetTarget(Interactable target)
    {
        _target = target;
    }

    public void RemoveTarget()
    {
        _target = null;
    }

    private void NeedMove(Vector3 point)
    {
        _targetPoint = point;
        _needMove = true;
    }
}
