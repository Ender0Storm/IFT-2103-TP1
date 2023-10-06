using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MinipotCollider : MonoBehaviour
{
    public bool m_IsTrigger;

    public abstract void OnCollisionEnter();
    public abstract void OnCollisionStay();
    public abstract void OnCollisionExit();
}
