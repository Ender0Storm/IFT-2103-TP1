using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinipotBounds
{
    public Vector3 center;
    public Vector3 size;

    public Vector3 max;
    public Vector3 min;

    public MinipotBounds(Vector3 p_center, Vector3 p_size)
    {
        center = p_center;
        size = p_size;

        max = center + size/2;
        min = center - size/2;
    }

    public Vector3 ClosestPoint(Vector3 p_point)
    {
        return new Vector3(Mathf.Clamp(p_point.x, min.x, max.x), Mathf.Clamp(p_point.y, min.y, max.y), Mathf.Clamp(p_point.z, min.z, max.z));
    }

    public float SqrDistance(Vector3 p_point)
    {
        return (p_point - ClosestPoint(p_point)).sqrMagnitude;
    }
}
