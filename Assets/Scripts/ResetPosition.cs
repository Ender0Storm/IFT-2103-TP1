
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public static ResetPosition instance;

    void Awake()
    {
        instance = this;
    }
    
    public GameObject ball = GameObject.Find("ball");
    private MinipotRigidbody minipotRigidbody;
    void Start()
    {
        Vector3 position = minipotRigidbody.GetPosition();
        ball.transform.position = position;
    }
    public void updatePos()
    {
        Vector3 position = minipotRigidbody.GetPosition();
        ball.transform.position = position;
    }
}