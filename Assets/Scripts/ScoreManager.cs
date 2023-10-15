using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text holeText;

    public GameObject[] holePrefabs;
    private int holeNumber;
    private GameObject currentHole;
    private double _score;
    
    void Start()
    {
        scoreText.text = "Shots : " + _score;

        currentHole = Instantiate(holePrefabs[0]);
        holeNumber = 1;
        holeText.text = "Hole " + holeNumber;
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
            // TODO: End condition
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