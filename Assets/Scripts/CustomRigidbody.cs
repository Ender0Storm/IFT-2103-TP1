using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public float m_Mass;
    public float m_Gravity;
    private Rigidbody rb;

    [NonSerialized]
    public Vector3 m_Velocity;
    [NonSerialized]
    public Vector3 m_Acceleration;
    [NonSerialized]
    public List<Vector3> m_Forces;

    // Start is called before the first frame update
    void Start()
    {
        m_Velocity = new Vector3();
        m_Acceleration = new Vector3();
        this.

        m_Forces = new List<Vector3>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Forces.Add(new Vector3(0, -m_Gravity * m_Mass, 0));

        m_Acceleration = new Vector3();
        foreach (Vector3 force in m_Forces) { m_Acceleration += force / m_Mass; }

        transform.position += m_Velocity * Time.fixedDeltaTime + m_Acceleration / 2 * Mathf.Pow(Time.fixedDeltaTime, 2);
        m_Velocity += m_Acceleration * Time.fixedDeltaTime;

        m_Forces.Clear();
    }
}
