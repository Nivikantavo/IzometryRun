using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayCheck : MonoBehaviour
{
    [SerializeField] private int _rays;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _angle;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _ignorLayer;
    [Range(0, 0.99f)] [SerializeField] private float _distanceMultiplier;

    private Player _target;
    private Vector3 _lastTargetPosition;
    private bool _targetIsVisible;

    public bool TargetIsVisible => _targetIsVisible;

    public event UnityAction Alarm;
    public event UnityAction<Vector3> TargetOutSight;

    private void Start()
    {
        _target = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _maxDistance)
        {
            if (RayToScan())
            {
                Alarm?.Invoke();
                _targetIsVisible = true;
            }
            else if(RayToScan() == false && _targetIsVisible)
            {
                TargetOutSight?.Invoke(_lastTargetPosition);
                _targetIsVisible = false;
            }
        }
    }

    private bool RayToScan()
    {
        bool result = false;
        bool a = false;
        bool b = false;
        float rayStep = 0;
        float distanceMultiplier;
        for (int i = 0; i < _rays; i++)
        {
            var x = Mathf.Sin(rayStep);
            var y = Mathf.Cos(rayStep);
            distanceMultiplier = Mathf.Pow(_distanceMultiplier, i);
            
            rayStep += _angle * Mathf.Deg2Rad / _rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));

            if (GetRaycast(dir, _maxDistance * distanceMultiplier))
                a = true;

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                if (GetRaycast(dir, _maxDistance * distanceMultiplier))
                    b = true;
            }
        }

        Debug.DrawRay(transform.position + _offset, _target.transform.position - transform.position);
        if (a || b) result = true;
        return result;
    }

    private bool GetRaycast(Vector3 direction, float distance)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 position = transform.position + _offset;
        

        if (Physics.Raycast(position, direction, out hit, distance, ~_ignorLayer))
        {
            if (hit.transform == _target.transform)
            {
                result = true;
                _lastTargetPosition = _target.transform.position;
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, direction * distance, Color.red);
        }
        return result;
    }
}
