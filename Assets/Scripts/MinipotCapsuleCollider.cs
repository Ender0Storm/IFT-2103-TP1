using UnityEngine;
public class MinipotCapsuleCollider : MinipotCollider
{
    public Bounds m_CollisionBox;
    public Vector3 m_Scale;

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