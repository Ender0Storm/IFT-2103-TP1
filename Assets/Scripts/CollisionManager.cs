using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private bool[] m_CurrentlyColliding;

    private MinipotCollider[] m_Colliders;
    private MinipotBallCollider m_BallCollider;
    private MinipotRigidbody m_BallRB;

    // Start is called before the first frame update
    void Start()
    {
        m_Colliders = GetComponentsInChildren<MinipotCollider>();

        m_CurrentlyColliding = new bool[m_Colliders.Length];
        m_BallCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<MinipotBallCollider>();
        m_BallRB = GameObject.FindGameObjectWithTag("Player").GetComponent<MinipotRigidbody>();
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
                    m_BallCollider.m_OnCollisionStay.Invoke();
                    collider.m_OnCollisionStay.Invoke();
                }
                else
                {
                    m_CurrentlyColliding[i] = true;

                    if (!collider.m_IsTrigger) { BounceBallOf(collider); }

                    m_BallCollider.m_OnCollisionEnter.Invoke();
                    collider.m_OnCollisionEnter.Invoke();
                }
            }
            else if (m_CurrentlyColliding[i])
            {
                m_CurrentlyColliding[i] = false;
                m_BallCollider.m_OnCollisionExit.Invoke();
                collider.m_OnCollisionExit.Invoke();
            }
        }
    }

    private void BounceBallOf(MinipotCollider collider)
    {
        Vector3 ballImpactPos = Vector3.up;
        Vector3 impactNormal = Vector3.up;

        if (collider is MinipotBallCollider)
        {
            MinipotBallCollider ballCollider = (MinipotBallCollider)collider;

            Vector3 P = m_BallRB.m_OldPosition;
            Vector3 V = m_BallRB.transform.position - P;
            Vector3 C = ballCollider.transform.position;
            float R = ballCollider.transform.localScale.x / 2 * ballCollider.m_Scale + m_BallCollider.transform.localScale.x / 2 * m_BallCollider.m_Scale;

            float alpha = LineBallIntersectAlpha(P, V, C, R);
            ballImpactPos = P + alpha * V;
            impactNormal = (ballImpactPos - C).normalized;
        }
        else if (collider is MinipotBoxCollider)
        {
            MinipotBoxCollider boxCollider = (MinipotBoxCollider)collider;
            Bounds bounds = boxCollider.m_CollisionBox;
            Bounds extendedBounds = new Bounds(bounds.center,
                                               bounds.size + m_BallCollider.transform.localScale * m_BallCollider.m_Scale);

            Vector3 rotatedPos = boxCollider.WorldToBoundingBox(m_BallRB.transform.position);
            Vector3 oldRotatedPos = boxCollider.WorldToBoundingBox(m_BallRB.m_OldPosition);

            Vector3 P = oldRotatedPos;
            Vector3 V = rotatedPos - P;
            float alpha;

            bool cornerCase = false;
            Vector3 cornerCheckPos;

            if (oldRotatedPos.x <= extendedBounds.max.x && oldRotatedPos.x >= extendedBounds.min.x &&
                oldRotatedPos.y <= extendedBounds.max.y && oldRotatedPos.y >= extendedBounds.min.y &&
                oldRotatedPos.z <= extendedBounds.max.z && oldRotatedPos.z >= extendedBounds.min.z)
            {
                ballImpactPos = oldRotatedPos;
                cornerCheckPos = bounds.ClosestPoint(ballImpactPos);
                cornerCase = true;
            }
            else
            {

                float[] hits = new float[6];
                for (int i = 0; i < hits.Length; i++) { hits[i] = float.MaxValue; }
                Vector3 hit;

                if (V.x != 0)
                {
                    alpha = (extendedBounds.max.x - P.x) / V.x;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.y <= extendedBounds.max.y && hit.y >= extendedBounds.min.y &&
                        hit.z <= extendedBounds.max.z && hit.z >= extendedBounds.min.z) { hits[0] = alpha; }

                    alpha = (extendedBounds.min.x - P.x) / V.x;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.y <= extendedBounds.max.y && hit.y >= extendedBounds.min.y &&
                        hit.z <= extendedBounds.max.z && hit.z >= extendedBounds.min.z) { hits[1] = alpha; }
                }
                if (V.y != 0)
                {
                    alpha = (extendedBounds.max.y - P.y) / V.y;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.x <= extendedBounds.max.x && hit.x >= extendedBounds.min.x &&
                        hit.z <= extendedBounds.max.z && hit.z >= extendedBounds.min.z) { hits[2] = alpha; }

                    alpha = (extendedBounds.min.y - P.y) / V.y;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.x <= extendedBounds.max.x && hit.x >= extendedBounds.min.x &&
                        hit.z <= extendedBounds.max.z && hit.z >= extendedBounds.min.z) { hits[3] = alpha; }
                }
                if (V.z != 0)
                {
                    alpha = (extendedBounds.max.z - P.z) / V.z;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.x <= extendedBounds.max.x && hit.x >= extendedBounds.min.x &&
                        hit.y <= extendedBounds.max.y && hit.y >= extendedBounds.min.y) { hits[4] = alpha; }

                    alpha = (extendedBounds.min.z - P.z) / V.z;
                    hit = P + alpha * V;
                    if (alpha >= 0 &&
                        hit.x <= extendedBounds.max.x && hit.x >= extendedBounds.min.x &&
                        hit.y <= extendedBounds.max.y && hit.y >= extendedBounds.min.y) { hits[5] = alpha; }
                }

                alpha = Mathf.Min(hits);

                ballImpactPos = P + alpha * V;
                cornerCheckPos = bounds.ClosestPoint(ballImpactPos);

                if (((cornerCheckPos.x == bounds.min.x || cornerCheckPos.x == bounds.max.x) && (cornerCheckPos.y == bounds.min.y || cornerCheckPos.y == bounds.max.y)) ||
                    ((cornerCheckPos.y == bounds.min.y || cornerCheckPos.y == bounds.max.y) && (cornerCheckPos.z == bounds.min.z || cornerCheckPos.z == bounds.max.z)) ||
                    ((cornerCheckPos.x == bounds.min.x || cornerCheckPos.x == bounds.max.x) && (cornerCheckPos.z == bounds.min.z || cornerCheckPos.z == bounds.max.z)))
                {
                    cornerCase = true;
                }
            }

            if (cornerCase)
            {
                float R = m_BallCollider.transform.localScale.x / 2 * m_BallCollider.m_Scale;
                alpha = 0;

                if ((cornerCheckPos.x == bounds.min.x || cornerCheckPos.x == bounds.max.x) &&
                    (cornerCheckPos.y == bounds.min.y || cornerCheckPos.y == bounds.max.y) &&
                    cornerCheckPos.z > bounds.min.z && cornerCheckPos.z < bounds.max.z)
                {
                    alpha = LineBallIntersectAlpha(new Vector3(P.x, P.y), new Vector3(V.x, V.y), new Vector3(cornerCheckPos.x, cornerCheckPos.y), R);
                    cornerCheckPos = bounds.ClosestPoint(P + alpha * V);
                }
                else if ((cornerCheckPos.y == bounds.min.y || cornerCheckPos.y == bounds.max.y) &&
                         (cornerCheckPos.z == bounds.min.z || cornerCheckPos.z == bounds.max.z) &&
                         cornerCheckPos.x > bounds.min.x && cornerCheckPos.x < bounds.max.x)
                {
                    alpha = LineBallIntersectAlpha(new Vector3(P.y, P.z), new Vector3(V.y, V.z), new Vector3(cornerCheckPos.y, cornerCheckPos.z), R);
                    cornerCheckPos = bounds.ClosestPoint(P + alpha * V);
                }
                else if ((cornerCheckPos.x == bounds.min.x || cornerCheckPos.x == bounds.max.x) &&
                         (cornerCheckPos.z == bounds.min.z || cornerCheckPos.z == bounds.max.z) &&
                         cornerCheckPos.y > bounds.min.y && cornerCheckPos.y < bounds.max.y)
                {

                    alpha = LineBallIntersectAlpha(new Vector3(P.x, P.z), new Vector3(V.x, V.z), new Vector3(cornerCheckPos.x, cornerCheckPos.z), R);
                    cornerCheckPos = bounds.ClosestPoint(P + alpha * V);
                }

                if ((cornerCheckPos.x == bounds.min.x || cornerCheckPos.x == bounds.max.x) &&
                    (cornerCheckPos.y == bounds.min.y || cornerCheckPos.y == bounds.max.y) &&
                    (cornerCheckPos.z == bounds.min.z || cornerCheckPos.z == bounds.max.z))
                {
                    alpha = LineBallIntersectAlpha(P, V, cornerCheckPos, R);
                }

                ballImpactPos = P + alpha * V;
            }

            Vector3 closestPointPos = boxCollider.BoundingBoxToWorld(bounds.ClosestPoint(ballImpactPos));
            ballImpactPos = boxCollider.BoundingBoxToWorld(ballImpactPos);
            impactNormal = (ballImpactPos - closestPointPos).normalized;
        }

        m_BallRB.BallBounceOff(impactNormal, ballImpactPos);
    }

    private float LineBallIntersectAlpha(Vector3 linePoint, Vector3 lineVector, Vector3 ballCenter, float ballRadius)
    {
        float a = lineVector.sqrMagnitude;
        float b = 2 * (Vector3.Dot(linePoint, lineVector) - Vector3.Dot(ballCenter, lineVector));
        float c = ballCenter.sqrMagnitude + linePoint.sqrMagnitude - 2 * Vector3.Dot(ballCenter, linePoint) - Mathf.Pow(ballRadius, 2);
        float d = Mathf.Pow(b, 2) - 4 * a * c;

        float e = Mathf.Sqrt(d);
        float f = 2 * a;

        return Mathf.Min((-b + e) / f, (-b - e) / f);
    }
}
