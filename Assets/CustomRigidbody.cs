using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public float m_Mass;
    public float m_Gravity;

    Vector3 m_Velocity;
    Vector3 m_Acceleration;
    List<Vector3> m_Forces;

    // Start is called before the first frame update
    void Start()
    {
        m_Velocity = new Vector3();
        m_Acceleration = new Vector3();
        m_Forces = new List<Vector3>();
        m_Forces.Add(new Vector3(0, -m_Gravity * m_Mass, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float interval = Time.fixedDeltaTime;
        Vector3 totalForce = new Vector3();
        foreach (Vector3 force in m_Forces) { totalForce += force; }

        m_Acceleration = totalForce / m_Mass;
        transform.position += m_Velocity * interval + m_Acceleration / 2 * Mathf.Pow(interval, 2);
        m_Velocity += m_Acceleration * interval;
    }
}
