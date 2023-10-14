using UnityEngine;

public class BallController : MonoBehaviour
{
    public float velocityCoef;
    public float maxHoldTime;
    
    private GameObject cameraObject;
    private MinipotRigidbody minipotRigidbody;
    private bool isPlayerTurn;
    private bool pressedDuringTurn;
    
    private float holdTime;
    private float immobileTime;

    void Start()
    {
        cameraObject = GameObject.Find("main_camera");
        minipotRigidbody = GetComponent<MinipotRigidbody>();
        isPlayerTurn = false;
        pressedDuringTurn = false;
    }
    
    void Update()
    {
        if (minipotRigidbody.m_Velocity.sqrMagnitude < 0.1)
        {
            if(immobileTime > 0.5)
            {
                minipotRigidbody.m_Velocity = Vector3.zero;
                isPlayerTurn = true;
            }
        }
        else
        {
            immobileTime = 0;
        }

        if (Input.GetKeyDown("space") && isPlayerTurn)
        {
            holdTime = 0;
            pressedDuringTurn = true;
        }

        if (Input.GetKeyUp("space") && isPlayerTurn && pressedDuringTurn)
        {
            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime) * velocityCoef;
            var directionX = cameraObject.transform.forward.x;
            var directionZ = cameraObject.transform.forward.z;
            minipotRigidbody.SetVelocity(new Vector3(directionX * holdTime, 0, directionZ * holdTime));
            holdTime = 0;
            ScoreManager.Instance.AddShot();
            isPlayerTurn = false;
            pressedDuringTurn = false;
        }

        holdTime += Time.deltaTime;
        immobileTime += Time.deltaTime;

        /*if (Input.GetKeyDown("n"))
        {
            transform.position = new Vector3(0, 0.5f, 0);
        }*/
    }
}