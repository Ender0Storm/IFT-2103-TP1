using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float velocityCoef;
    public float maxHoldTime;
    
    public ScoreManager scoreManager;
    public GameObject cameraObject;
    public MinipotRigidbody ballRB;
    public PowerManager powerManager ;
    private bool isPlayerTurn;
    private bool pressedDuringTurn;
    public Text playerTurnText;
    
    private float holdTime;
    private float immobileTime;

    private Vector3 lastPosition;

    void Start()
    {
        ballRB = GetComponent<MinipotRigidbody>();
        isPlayerTurn = false;
        pressedDuringTurn = false;
        playerTurnText.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (ballRB.m_Velocity.sqrMagnitude < 0.1)
        {
            if(immobileTime > 0.5 && !pressedDuringTurn)
            {
                ballRB.m_Velocity = Vector3.zero;
                isPlayerTurn = true;
                playerTurnText.gameObject.SetActive(true);
            }
        }
        else
        {
            immobileTime = 0;
        }

        if (Input.GetKeyDown("space") && isPlayerTurn && !scoreManager.gameFinished)
        {
            holdTime = 0;
            playerTurnText.gameObject.SetActive(false);
            powerManager.SetPower(holdTime);
            pressedDuringTurn = true;
        }

        if (pressedDuringTurn)
        {
            powerManager.SetPower(holdTime);
        }

        if (Input.GetKeyUp("space") && isPlayerTurn && pressedDuringTurn && !scoreManager.gameFinished)
        {
            lastPosition = transform.position;

            holdTime = Mathf.Clamp(holdTime, 0, maxHoldTime) * velocityCoef;
            var directionX = cameraObject.transform.forward.x;
            var directionZ = cameraObject.transform.forward.z;
            ballRB.SetVelocity(new Vector3(directionX * holdTime, 0, directionZ * holdTime));
            holdTime = 0;
            powerManager.SetPower(0);
            scoreManager.AddShot();
            isPlayerTurn = false;
            pressedDuringTurn = false;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        holdTime += Time.deltaTime;
        immobileTime += Time.deltaTime;

        if (Input.GetKeyDown("n") && scoreManager.gameFinished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    public void HoleComplete()
    {
        scoreManager.ChangeHole();

        ballRB.m_Velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
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