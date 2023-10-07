using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;

    public double score = 0;
    
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        scoreText.text = "Shots : " + score;
    }

    public void AddShot()
    {
        score += 0.5;
        scoreText.text = "Shots : " + score;
    }
}