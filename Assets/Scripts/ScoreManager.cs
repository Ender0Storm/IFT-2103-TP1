using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    public Text scoreHole1;
    public Text scoreHole2;
    public Text scoreHole3;
    public Text total;
    

    private double _score;
    
    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        scoreText.text = "Shots : " + _score;
        scoreHole1.text = "1       ";
        scoreHole2.text = "2       ";
        scoreHole3.text = "3       ";
        total.text = "Total   ";
    }

    public void AddShot()
    {
        _score += 1;
        scoreText.text = "Shots : " + _score;
    }

    public void UpdateHoleScore(int hole)
    {
        switch (hole)
        {
            case 1:
                scoreHole1.text = "1       " + _score;
                break;
            case 2:
                scoreHole2.text = "2       " + _score;
                break;
            case 3:
                scoreHole3.text = "3       " + _score;
                break;
            default:
                total.text = "Total   " + _score;
                break;
        }
        _score = 0;
    }

    public void ResetScore()
    {
        _score = 0;
        scoreHole1.text = "1       ";
        scoreHole2.text = "2       ";
        scoreHole3.text = "3       ";
        total.text = "Total   ";
    }
}