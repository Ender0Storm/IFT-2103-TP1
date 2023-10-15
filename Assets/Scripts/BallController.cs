using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    public static BallController Instance;
    
    public float velocityCoef;
    public float maxHoldTime;
    
    public ScoreManager scoreManager;
    public GameObject cameraObject;
    public MinipotRigidbody ballRB;
    private bool isPlayerTurn;
    private bool pressedDuringTurn;
    private bool gameFinished;
    
    private float holdTime;
    private float immobileTime;

    private Vector3 lastPosition;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        isPlayerTurn = false;
        pressedDuringTurn = false;
        gameFinished = false;
    }
    
    void Update()
    {
        if (ballRB.m_Velocity.sqrMagnitude < 0.1)
        {
            if(immobileTime > 0.5)
            {
                ballRB.m_Velocity = Vector3.zero;
                isPlayerTurn = true;
            }
        }
        else
        {
            immobileTime = 0;
        }

        if (Input.GetKeyDown("space") && isPlayerTurn && !gameFinished)
        {
            holdTime = 0;
            // TODO: Show ball strength
            pressedDuringTurn = true;
        }

        if (Input.GetKeyUp("space") && isPlayerTurn && pressedDuringTurn && !gameFinished)
        {
            lastPosition = transform.position;

            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime) * velocityCoef;
            var directionX = cameraObject.transform.forward.x;
            var directionZ = cameraObject.transform.forward.z;
            ballRB.SetVelocity(new Vector3(directionX * holdTime, 0, directionZ * holdTime));
            holdTime = 0;
            scoreManager.AddShot();
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

    public void HoleComplete()
    {
        scoreManager.ChangeHole();

        ballRB.m_Velocity = Vector3.zero;
        transform.position = Vector3.up;
    }

    public void ResetPosition()
    {
        lastPosition.y += 0.1f;
        transform.position = lastPosition;
        ballRB.m_Velocity = Vector3.zero;
        scoreManager.AddShot();
    }
}