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
            // On applique l'inverse de la rotation de la boite sur la position de la sphère pour simplifier le calcule (un seul point à bouger au lieu de la boite au complet)
            Vector3 rotatedPos = boxCollider.WorldToBoundingBox(transform.position);

            return boxCollider.m_CollisionBox.SqrDistance(rotatedPos) <= Mathf.Pow(m_Scale * transform.localScale.x / 2, 2);
        }

        return false;
    }
}
