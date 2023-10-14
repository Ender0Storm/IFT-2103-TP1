using UnityEngine;

public class MinipotBoxCollider : MinipotCollider
{
    public Vector3 m_Scale;
    [HideInInspector]
    public MinipotBounds m_CollisionBox;

    void Start()
    {
        m_CollisionBox = new MinipotBounds(transform.position, Vector3.Scale(transform.localScale, m_Scale));
    }

    public Vector3 WorldToBounds(Vector3 point)
    {
        return Quaternion.Inverse(transform.localRotation) * (point - transform.position) + transform.position;
    }

    public Vector3 BoundsToWorld(Vector3 point)
    {
        return transform.localRotation * (point - transform.position) + transform.position;
    }
}
