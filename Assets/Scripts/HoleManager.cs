using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    private BallController ballController;

    // Start is called before the first frame update
    void Start()
    {
        ballController = GameObject.FindGameObjectWithTag("Player").GetComponent<BallController>();
    }

    public void ResetBall()
    {
        ballController.ResetPosition();
    }

    public void CompleteHole()
    {
        ballController.HoleComplete();
    }
}
