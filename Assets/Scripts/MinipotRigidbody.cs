using System;
using System.Collections.Generic;
using UnityEngine;

public class MinipotRigidbody : MonoBehaviour
{
    public float m_Mass;
    public float m_Gravity;
    [Range(0, 1)]
    public float m_BouncinessRatio;
    [Range(0, 1)]
    public float m_FrictionRatio;

    [NonSerialized]
    public bool m_IsGrounded;
    [NonSerialized]
    public Vector3 m_GroundedNormal;
    private Vector3 m_Velocity;
    private Vector3 m_Acceleration;
    private List<Vector3> m_Forces;
    [NonSerialized]
    public Vector3 m_OldPosition;
    ResetPosition resetPosition = ResetPosition.instance;

    private Vector3 position = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        m_Velocity = new Vector3();
        m_Acceleration = new Vector3();
        m_Forces = new List<Vector3>();
        m_OldPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(m_Velocity);
        m_Forces.Add(new Vector3(0, -m_Gravity * m_Mass, 0));
        if (m_IsGrounded && Mathf.Abs(m_Velocity.y) < 0.1)
        {
            m_Forces.Add(m_GroundedNormal * m_Gravity * m_Mass);
            m_Forces.Add((m_Velocity - Vector3.Project(m_Velocity, m_GroundedNormal)) * -m_FrictionRatio);
            m_IsGrounded = false;
        }

        m_Acceleration = new Vector3();
        foreach (Vector3 force in m_Forces) { m_Acceleration += force / m_Mass; }

        m_OldPosition = transform.position;
        transform.position += m_Velocity * Time.fixedDeltaTime + m_Acceleration / 2 * Mathf.Pow(Time.fixedDeltaTime, 2);
        m_Velocity += m_Acceleration * Time.fixedDeltaTime;

        m_Forces.Clear();
        
        //print(m_Velocity);
        
        if (m_Velocity == Vector3.zero)
        {
            //print("position zero");
            position = transform.position;
        }
    }

    public void BallBounceOff(Vector3 normal, Vector3 ballImpactPos)
    {
        m_Velocity = m_BouncinessRatio * (m_Velocity - 2 * Vector3.Dot(normal, m_Velocity) * normal);
        if (m_Velocity.y < 0.5 && m_Velocity.y > 0) { m_Velocity.y = 0; }
        transform.position = ballImpactPos + (transform.position - ballImpactPos).magnitude * m_BouncinessRatio * m_Velocity.normalized;
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

    public void setVelocity(Vector3 velocity)
    {
        m_Velocity = velocity;
    }

    public void addForce(Vector3 force) 
    {
        m_Forces.Add(force);
    }

    public Vector3 getPosition()
    {
        return position;
    }
}
