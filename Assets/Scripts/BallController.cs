using UnityEngine;

public class BallController : MonoBehaviour
{
    public int velocityCoef;
    
    
    private GameObject cameraObject;
    private MinipotRigidbody minipotRigidbody;
    
    private float deltaTime;
    private float startTime;

    void Start()
    {
        cameraObject = GameObject.Find("main_camera");
        minipotRigidbody = GetComponent<MinipotRigidbody>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            startTime = Time.time;
        }

        if (Input.GetKeyUp("space") && startTime > 0)
        {
            deltaTime = (Time.time - startTime < 1 ? Time.time - startTime : 1) * velocityCoef;
            var directionX = cameraObject.transform.forward.x;
            var directionZ = cameraObject.transform.forward.z;
            minipotRigidbody.setVelocity(new Vector3(directionX*deltaTime, 0.5f, directionZ*deltaTime));
            deltaTime = 0;
            ScoreManager.Instance.AddShot();
        }

        /*if (Input.GetKeyDown("n"))
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }*/
    }
}