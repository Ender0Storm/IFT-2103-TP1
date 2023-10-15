using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public static BallController Instance;
    
    public float velocityCoef;
    public float maxHoldTime;
    
    private GameObject cameraObject;
    private MinipotRigidbody minipotRigidbody;
    private bool isPlayerTurn;
    private bool pressedDuringTurn;
    private bool gameFinished;
    
    private float holdTime;
    private float immobileTime;
    private Object hole1;
    private int holeNumber = 1;
    private Vector3 lastPosition;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        hole1 = GameObject.Find("first_hole");
        cameraObject = GameObject.Find("main_camera");
        minipotRigidbody = GetComponent<MinipotRigidbody>();
        isPlayerTurn = false;
        pressedDuringTurn = false;
        gameFinished = false;
        lastPosition = transform.position;
    }
    
    void Update()
    {
        if (minipotRigidbody.m_Velocity.sqrMagnitude < 0.1)
        {
            if(immobileTime > 0.5)
            {
                minipotRigidbody.m_Velocity = Vector3.zero;
                isPlayerTurn = true;
                lastPosition = transform.position;
            }
        }
        else
        {
            immobileTime = 0;
        }

        if (Input.GetKeyDown("space") && isPlayerTurn && !gameFinished)
        {
            holdTime = 0;
            pressedDuringTurn = true;
        }

        if (Input.GetKeyUp("space") && isPlayerTurn && pressedDuringTurn && !gameFinished)
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

        if (Input.GetKeyDown("n") && !gameFinished)
        {
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetPosition()
    {
        lastPosition.y += 0.5f;
        transform.position = lastPosition;
    }

    public void ChangeHole()
    {
        var hole2 = (GameObject)Resources.Load("Prefabs/second_hole", typeof(GameObject));
        switch (holeNumber)
        {
            case 1:
                Destroy(hole1);
                Instantiate(hole2, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case 2:
                Destroy(hole2);
                break;
            case 3:
                Destroy(hole1);
                break;
            default:
                holeNumber = 1;
                Destroy(hole1);
                gameFinished = true;
                break;
        }
        
        if (!gameFinished)
        {
            transform.position = new Vector3(0, 2f, 0);
            ScoreManager.Instance.UpdateHoleScore(holeNumber);
            holeNumber++;
        }
    }
}