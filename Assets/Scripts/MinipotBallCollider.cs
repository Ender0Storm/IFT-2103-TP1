using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinipotBallCollider : MinipotCollider
{
    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionStay()
    {
        throw new System.NotImplementedException();
    }

    public bool collidesWith(MinipotCollider other)
    {
        return false;
    }
}
