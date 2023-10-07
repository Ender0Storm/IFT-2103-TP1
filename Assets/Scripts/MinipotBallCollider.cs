using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinipotBallCollider : MinipotCollider
{
    public float m_Scale;

    public bool CollidesWith(MinipotCollider other)
    {
        if (other is MinipotBallCollider)
        {
            MinipotBallCollider ballCollider = (MinipotBallCollider)other;

            return (ballCollider.transform.position - transform.position).sqrMagnitude <=
                    Mathf.Pow(ballCollider.m_Scale * ballCollider.transform.localScale.x + m_Scale * transform.localScale.x, 2);
        }
        else if (other is MinipotBoxCollider)
        {
            MinipotBoxCollider boxCollider = (MinipotBoxCollider)other;

            return (boxCollider.m_CollisionBox.ClosestPoint(transform.position) - transform.position).sqrMagnitude <= Mathf.Pow(m_Scale * transform.localScale.x, 2);
        }

        return false;
    }
}
