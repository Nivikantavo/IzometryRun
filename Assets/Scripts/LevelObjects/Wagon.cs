using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Wagon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private LevelButton _wagonButton;
    [SerializeField] private Transform[] _paths;
    [SerializeField] private float _speed;
    
    private int _currentPath;
    private float _tParameter;
    private bool _corutineAllowed;

    private void Awake()
    {
        _currentPath = 0;
        _tParameter = 0;
        _corutineAllowed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Humanoid>(out var humanoid))
        {
            humanoid.TakeDamage(_damage);
        }
    }

    private void OnEnable()
    {
        _wagonButton.ButtonClicked += OnStartMoving;
    }

    private void OnDisable()
    {
        _wagonButton.ButtonClicked -= OnStartMoving;
    }

    private void OnStartMoving()
    {
        _corutineAllowed = true;
    }

    private void Update()
    {
        if (_corutineAllowed)
        {
            StartCoroutine(MoveWagon(_currentPath));
        }
    }

    private IEnumerator MoveWagon(int pathNumber)
    {
        WaitForSeconds _step = new WaitForSeconds(0.005f);
        _corutineAllowed = false;
        Vector3[] currentPathPoints = new Vector3[4];

        for (int i = 0; i < 4; i++)
        {
            currentPathPoints[i] = _paths[pathNumber].GetChild(i).position;
        }

        while(_tParameter < 1)
        {
            _tParameter += Time.deltaTime * _speed;
            transform.position = Bezier.GetPoint(currentPathPoints[0], currentPathPoints[1], currentPathPoints[2], currentPathPoints[3], _tParameter);
            transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(currentPathPoints[0], currentPathPoints[1], currentPathPoints[2], currentPathPoints[3], _tParameter));

            yield return _step;
        }

        _tParameter = 0f;

        if (_currentPath < _paths.Length - 1)
        {
            _currentPath += 1;
            _corutineAllowed = true;
            yield break;
        }
    }
}
