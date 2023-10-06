using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private bool[] m_CurrentlyColliding;

    public MinipotCollider[] m_colliders;
    private MinipotBallCollider m_ballCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentlyColliding = new bool[m_colliders.Length];
        m_ballCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<MinipotBallCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            MinipotCollider collider = m_colliders[i];

            if (m_ballCollider.collidesWith(collider))
            {
                if (m_CurrentlyColliding[i])
                {
                    m_ballCollider.OnCollisionStay();
                    collider.OnCollisionStay();
                } else
                {
                    m_CurrentlyColliding[i] = true;
                    m_ballCollider.OnCollisionEnter();
                    collider.OnCollisionEnter();
                }
            }

            if (m_CurrentlyColliding[i])
            {
                m_CurrentlyColliding[i] = false;
                m_ballCollider.OnCollisionExit();
                collider.OnCollisionExit();
            }
        }
    }
}
