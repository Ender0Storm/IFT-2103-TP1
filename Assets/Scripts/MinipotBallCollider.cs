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
                    Mathf.Pow(ballCollider.m_Scale * ballCollider.transform.localScale.x / 2 + m_Scale * transform.localScale.x / 2, 2);
        }
        else if (other is MinipotBoxCollider)
        {
            MinipotBoxCollider boxCollider = (MinipotBoxCollider)other;
            // On applique l'inverse de la rotation de la boite sur la position de la sph�re pour simplifier le calcule (un seul point � bouger au lieu de la boite au complet)
            Vector3 rotatedPos = Quaternion.Inverse(boxCollider.transform.localRotation) * (transform.position - boxCollider.transform.position) + boxCollider.transform.position;

            return (boxCollider.m_CollisionBox.ClosestPoint(rotatedPos) - rotatedPos).sqrMagnitude <= Mathf.Pow(m_Scale * transform.localScale.x / 2, 2);
        }

        return false;
    }
}
