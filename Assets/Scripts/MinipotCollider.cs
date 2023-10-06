using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class MinipotCollider : MonoBehaviour
{
    public bool m_IsTrigger;

    public UnityEvent m_OnCollisionEnter;
    public UnityEvent m_OnCollisionStay;
    public UnityEvent m_OnCollisionExit;
}
