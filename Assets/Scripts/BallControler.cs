using UnityEngine;

public class BallControler : MonoBehaviour
{
    private GameObject cameraObject;
    private MinipotRigidbody minipotRigidbody;
    public float deltaTime;
    private ScoreManager scoreManager = ScoreManager.instance;
    
    public int speed = 2;
    public float startTime;

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
            deltaTime = (Time.time - startTime < 2.5f ? Time.time - startTime : 2.5f) * 10;
            float direction_x = cameraObject.transform.forward.x;
            float direction_z = cameraObject.transform.forward.z;
            print(deltaTime + " ");
            minipotRigidbody.setVelocity(new Vector3(direction_x*deltaTime, 0, direction_z*deltaTime));
            deltaTime = 0;
            scoreManager.AddShot();
        }
    }
}