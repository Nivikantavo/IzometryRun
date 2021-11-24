using UnityEngine;

[ExecuteAlways]
public class BezierMovment : MonoBehaviour
{
    [SerializeField] private Transform[] _pathPoints;

    [Range(0,1)]
    [SerializeField] private float _distanceTraveled;

    private void Update()
    {
        MoveAlongPath(_pathPoints, _distanceTraveled);
    }

    private void MoveAlongPath(Transform[] path, float distanceTraveled)
    {
        transform.position = Bezier.GetPoint(path[0].position, path[1].position, path[2].position, path[3].position, distanceTraveled);
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(path[0].position, path[1].position, path[2].position, _pathPoints[3].position, distanceTraveled));
    }


    private void OnDrawGizmos() 
    {

        int sigmentsNumber = 20;
        Vector3 preveousePoint = _pathPoints[0].position;

        for (int i = 0; i < sigmentsNumber + 1; i++) {
            float distanceTraveled = (float)i / sigmentsNumber;
            Vector3 point = Bezier.GetPoint(_pathPoints[0].position, _pathPoints[1].position, _pathPoints[2].position, _pathPoints[3].position, distanceTraveled);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
        }

    }

}
