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
}
