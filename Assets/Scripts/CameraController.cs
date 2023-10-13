using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform ball;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;
    private bool movingRight;
    private bool movingLeft;
    [Range(1,100)]
    public int rotation_speed = 35;
    
    
    void Start()
    {
        ball = GameObject.Find("ball").transform;
    }

    void Update () {
        if (Input.GetKeyDown("a"))
        {
            movingRight = true;
            movingLeft = false;
        }

        if (Input.GetKeyUp("a"))
        {
            movingRight = false;
        }
        
        if (Input.GetKeyUp("d"))
        {
            movingLeft = false;
        }

        if (Input.GetKeyDown("d"))
        {
            movingRight = false;
            movingLeft = true;
        }

        if (movingRight)
        {
            ball.transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime);
        }

        if (movingLeft)
        {
            ball.transform.Rotate(Vector3.down * rotation_speed * Time.deltaTime);
        }
    }
}