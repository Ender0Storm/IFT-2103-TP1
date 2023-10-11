using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinipotRigidbody : MonoBehaviour
{
    public float m_Mass;
    public float m_Gravity;
    [Range(0, 1)]
    public float m_Bounciness;

    private Vector3 m_Velocity;
    private Vector3 m_Acceleration;
    private List<Vector3> m_Forces;
    [NonSerialized]
    public Vector3 m_OldPosition;

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
        m_Forces.Add(new Vector3(0, -m_Gravity * m_Mass, 0));

        m_Acceleration = new Vector3();
        foreach (Vector3 force in m_Forces) { m_Acceleration += force / m_Mass; }

        m_OldPosition = transform.position;
        transform.position += m_Velocity * Time.fixedDeltaTime + m_Acceleration / 2 * Mathf.Pow(Time.fixedDeltaTime, 2);
        m_Velocity += m_Acceleration * Time.fixedDeltaTime;

        m_Forces.Clear();
    }

    public void BallBounceOff(Vector3 normal, Vector3 ballImpactPos)
    {
        m_Velocity = m_Bounciness * (m_Velocity - 2 * Vector3.Dot(normal, m_Velocity) * normal);
        transform.position = ballImpactPos + (transform.position - ballImpactPos).magnitude * m_Velocity.normalized;
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
