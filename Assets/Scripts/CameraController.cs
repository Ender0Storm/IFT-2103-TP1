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
    public int rotation_speed;
    private float ratio;
    public float maxRatio;
    public float speedUpRatio;
    
    
    void Start()
    {
        ball = GameObject.Find("ball").transform;
    }

    void Update () {
        if (Input.GetKeyDown("a"))
        {
            movingRight = true;
            movingLeft = false;
            ratio = 1;
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
            ratio = 1;
        }

        if (movingRight)
        {
            ball.transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime * ratio);
            ratio += Time.deltaTime * speedUpRatio;
            ratio = Mathf.Clamp(ratio, 1, maxRatio);
        }

        if (movingLeft)
        {
            ball.transform.Rotate(Vector3.down * rotation_speed * Time.deltaTime * ratio);
            ratio += Time.deltaTime * speedUpRatio;
            ratio = Mathf.Clamp(ratio, 1, maxRatio);
        }
    }
}