using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonPath : MonoBehaviour
{
    private Vector3[] _currentPathPoints = new Vector3[4];

    private void OnDrawGizmos()
    {
        for (int i = 0; i < 4; i++)
        {
            _currentPathPoints[i] = transform.GetChild(i).position;
        }

        int sigmentsNumber = 20;
        Vector3 preveousePoint = _currentPathPoints[0];

        for (int i = 0; i < sigmentsNumber + 1; i++)
        {
            float paremeter = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(_currentPathPoints[0], _currentPathPoints[1], _currentPathPoints[2], _currentPathPoints[3], paremeter);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }
    }
}
