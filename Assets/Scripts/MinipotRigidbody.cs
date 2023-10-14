using System;
using System.Collections.Generic;
using UnityEngine;

public class MinipotRigidbody : MonoBehaviour
{
    public float m_Mass;
    public float m_Gravity;
    [Range(0, 1)]
    public float m_Bounciness;
    public float m_FrictionCoefficient;

    [NonSerialized]
    public bool m_IsGrounded;
    [NonSerialized]
    private Vector3 m_GroundedNormal;

    public Vector3 m_Velocity;
    private Vector3 m_Acceleration;
    private List<Vector3> m_Forces;
    [NonSerialized]
    public Vector3 m_OldPosition;
    ResetPosition resetPosition = ResetPosition.instance;

    private Vector3 position = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        m_Acceleration = new Vector3();
        m_Forces = new List<Vector3>();
        m_OldPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Forces.Add(new Vector3(0, -m_Gravity * m_Mass, 0));
        if (m_IsGrounded)
        {
            m_Forces.Add(m_GroundedNormal * m_Gravity * m_Mass);
            Vector3 friction = (m_Velocity - Vector3.Project(m_Velocity, m_GroundedNormal)) * -m_FrictionCoefficient;
            m_Forces.Add(friction);
        }

        m_Acceleration = new Vector3();
        foreach (Vector3 force in m_Forces) { m_Acceleration += force / m_Mass; }

        m_OldPosition = transform.position;
        transform.position += m_Velocity * Time.fixedDeltaTime + m_Acceleration / 2 * Mathf.Pow(Time.fixedDeltaTime, 2);
        m_Velocity += m_Acceleration * Time.fixedDeltaTime;

        m_Forces.Clear();
        
        if (m_Velocity == Vector3.zero)
        {
            position = transform.position;
        }
    }

    public void BallBounceOff(Vector3 normal, Vector3 ballImpactPos)
    {
        m_Velocity = (1 - Mathf.Abs(Vector3.Dot(normal, m_Velocity.normalized)) * (1 - m_Bounciness)) * (m_Velocity - 2 * Vector3.Dot(normal, m_Velocity) * normal);

        if (m_Velocity.y < 0.5 && m_Velocity.y > 0 && m_Velocity.sqrMagnitude < 0.25) { m_Velocity.y = 0; }

        transform.position = ballImpactPos + (transform.position - ballImpactPos).magnitude * m_Bounciness * m_Velocity.normalized;
    }

    public void UpdateNormals(Vector3[] normals)
    {
        m_GroundedNormal = Vector3.zero;
        foreach (Vector3 normal in normals)
        {
            m_GroundedNormal += normal;
        }
        m_GroundedNormal.Normalize();

        if (m_GroundedNormal == Vector3.zero) { m_IsGrounded = false; }
        else { m_IsGrounded = true; }
    }

    public void SetVelocity(Vector3 velocity)
    {
        m_Velocity = velocity;
    }

    public void AddForce(Vector3 force) 
    {
        m_Forces.Add(force);
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public void LogEnter()
    {
        print("Entered!");
    }

    public void LogStay()
    {
        print("Staying!");
    }

    public void LogExit()
    {
        print("Exited!");
    }
}
