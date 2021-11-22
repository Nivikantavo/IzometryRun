using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionsRange;
    [SerializeField] private float _rangedSpread;

    private Vector3 _transitionPoint;
    private void Start()
    {
        _transitionsRange += Random.Range(-_rangedSpread, _rangedSpread);
        _transitionPoint = Target.transform.position;
        //_transitionPoint = new Vector3(Target.gameObject.GetComponent<CapsuleCollider>().bounds.extents.x, transform.position.y, Target.gameObject.GetComponent<CapsuleCollider>().bounds.extents.z);
    }
    
    private void Update()
    {
        if (Vector3.Distance(transform.position, _transitionPoint) < _transitionsRange)
            NeedTransit = true;
    }
}
