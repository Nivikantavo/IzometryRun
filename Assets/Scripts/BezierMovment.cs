using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierMovment : MonoBehaviour
{
    [SerializeField] private Transform[] _pathPoints;

    [Range(0,1)]
    public float t;

    void Update()
    {
        MoveAlongPath(_pathPoints, t);
    }

    private void MoveAlongPath(Transform[] path, float parameterT)
    {
        transform.position = Bezier.GetPoint(path[0].position, path[1].position, path[2].position, path[3].position, parameterT);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(path[0].position, path[1].position, path[2].position, _pathPoints[3].position, parameterT));
    }


    private void OnDrawGizmos() {

        int sigmentsNumber = 20;
        Vector3 preveousePoint = _pathPoints[0].position;

        for (int i = 0; i < sigmentsNumber + 1; i++) {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(_pathPoints[0].position, _pathPoints[1].position, _pathPoints[2].position, _pathPoints[3].position, paremeter);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }

    }

}
