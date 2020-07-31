using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject endGameScoreObject;

    TextMeshProUGUI endGameScore;

    int score;

    private void Awake ()
    {
        score = 0;
        endGameScore = endGameScoreObject.GetComponent<TextMeshProUGUI>();
        UpdateScoreValue(score);
    }

    public void AddToScore (int value)
    {
        score += value;
        UpdateScoreValue(score);
    }
    
    public void UpdateScoreValue (int score)
    {
        scoreText.text = score.ToString();
        endGameScore.text = scoreText.text;
    }
}
