using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private bool[] m_CurrentlyColliding;

    public MinipotCollider[] m_Colliders;
    private MinipotBallCollider m_BallCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentlyColliding = new bool[m_Colliders.Length];
        m_BallCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<MinipotBallCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_Colliders.Length; i++)
        {
            MinipotCollider collider = m_Colliders[i];

            if (m_BallCollider.CollidesWith(collider))
            {
                if (m_CurrentlyColliding[i])
                {
                    if (!collider.m_IsTrigger)
                    {
                        m_BallCollider.BounceOff(collider);
                    }
                    m_BallCollider.m_OnCollisionStay.Invoke();
                    collider.m_OnCollisionStay.Invoke();
                } else
                {
                    m_CurrentlyColliding[i] = true;
                    m_BallCollider.m_OnCollisionEnter.Invoke();
                    collider.m_OnCollisionEnter.Invoke();
                }
            }

            if (m_CurrentlyColliding[i])
            {
                m_CurrentlyColliding[i] = false;
                m_BallCollider.m_OnCollisionExit.Invoke();
                collider.m_OnCollisionExit.Invoke();
            }
        }
    }
}
