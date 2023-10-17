using System;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text holeText;
    public Text totalScoreText;

    public GameObject[] holePrefabs;
    private int holeNumber;
    private GameObject currentHole;
    private double _score;
    [NonSerialized]
    public bool gameFinished;

    void Start()
    {
        scoreText.text = "Shots : " + _score;

        currentHole = Instantiate(holePrefabs[0]);
        holeNumber = 1;
        holeText.text = "Hole " + holeNumber;
        totalScoreText.gameObject.SetActive(false);
        gameFinished = false;
    }

    public void AddShot()
    {
        _score += 1;
        scoreText.text = "Shots : " + _score;
    }

    public void ChangeHole()
    {
        if (holeNumber == holePrefabs.Length)
        {
            scoreText.gameObject.SetActive(false);
            holeText.gameObject.SetActive(false);
            totalScoreText.text = "Congrats !\nYou finished with a score of " + _score +
                                  " shots !\n\nPress \"n\" to play again !";
            totalScoreText.gameObject.SetActive(true);
            gameFinished = true;
        }
        else
        {
            Destroy(currentHole);
            currentHole = Instantiate(holePrefabs[holeNumber]);
            holeNumber++;
            holeText.text = "Hole " + holeNumber;
        }
    }
}