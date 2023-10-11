using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinipotBoxCollider : MinipotCollider
{
    public Vector3 m_Scale;
    [HideInInspector]
    public Bounds m_CollisionBox;

    void Start()
    {
        m_CollisionBox = new Bounds(transform.position, Vector3.Scale(transform.localScale, m_Scale));
    }

    public Vector3 WorldToBoundingBox(Vector3 point)
    {
        return Quaternion.Inverse(transform.localRotation) * (point - transform.position) + transform.position;
    }

    public Vector3 BoundingBoxToWorld(Vector3 point)
    {
        return transform.localRotation * (point - transform.position) + transform.position;
    }
}
