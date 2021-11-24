using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float currentPosition) 
    {
        currentPosition = Mathf.Clamp01(currentPosition);
        float oneMinusT = 1f - currentPosition;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * currentPosition * p1 +
            3f * oneMinusT * currentPosition * currentPosition * p2 +
            currentPosition * currentPosition * currentPosition * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float currentPosition) 
    {
        currentPosition = Mathf.Clamp01(currentPosition);
        float oneMinusT = 1f - currentPosition;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * currentPosition * (p2 - p1) +
            3f * currentPosition * currentPosition * (p3 - p2);
    }
}
