using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(RayCheck))]
[RequireComponent(typeof(Enemy))]
public class PatrolState : State
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private int _angelRotation = 260;
    private NavMeshAgent _agent;

    private float _speed;
    private float _offset;
    private int _currentPointNumber;
    private bool _isHolding = false;
    private bool _onWay = false;
    private WaitForSeconds _timeBetweenRotation = new WaitForSeconds(1f);
    private WaitForSeconds _timeStepRotation = new WaitForSeconds(0.00001f);
    private RayCheck _rayCheck;
    private Coroutine _lookAroundCor;

    public event UnityAction<Vector3> NeedMove;
    
    private void Awake()
    {
        _rayCheck = GetComponent<RayCheck>();
        _speed = GetComponent<Enemy>().StandartSpeed;
        _agent = GetComponent<NavMeshAgent>();
        _currentPointNumber = 0;
        _offset = 0.2f;
    }

    private void OnEnable()
    {
        _agent.speed = _speed;
        _rayCheck.Alarm += Alarm;
        _lookAroundCor = StartCoroutine(LookAround());
    }

    private void OnDisable()
    {
        _rayCheck.Alarm -= Alarm;

        if (_lookAroundCor != null)
            StopCoroutine(_lookAroundCor);
    }

    private void Update()
    {
        if(_isHolding == false)
        {
            if(_onWay == false)
            {
                NeedMove?.Invoke(_patrolPoints[_currentPointNumber].position);
                _onWay = true;
            }
        }
        
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_patrolPoints[_currentPointNumber].position.x, _patrolPoints[_currentPointNumber].position.z)) <= _offset)
        {
            if (_isHolding == false)
                _lookAroundCor = StartCoroutine(LookAround());
        }
    }

    private IEnumerator LookAround()
    {
        _isHolding = true;
        _agent.speed = 0;

        for(int i = -1; i <= 1; i += 2)
        {
            for(int j = 0; j < _angelRotation; j++)
            {
                transform.Rotate(new Vector3(0, 1 * i, 0));
                
                yield return _timeStepRotation;
            }
            yield return _timeBetweenRotation;
        }
        if (_currentPointNumber == _patrolPoints.Length - 1)
            _currentPointNumber = 0;
        else
            _currentPointNumber++;

        _agent.speed = _speed;
        _isHolding = false;
        _onWay = false;
    }

    private void Alarm()
    {
        if(_lookAroundCor != null)
            StopCoroutine(_lookAroundCor);
    }
}
