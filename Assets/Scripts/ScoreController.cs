using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject endGameScoreObject;

    public GameObject popupTemplate;

    TextMeshProUGUI endGameScore;

    int score;

    private void Awake ()
    {
        score = 0;
        endGameScore = endGameScoreObject.GetComponent<TextMeshProUGUI>();
        UpdateScoreValue(score);
    }

    public void AddToScore (Vector3 origin, int value)
    {
        Color textColor;
        if (value > 0)
        {
            textColor = Color.green;
        }
        else
        {
            textColor = Color.red;
        }

        score += value;
        if (score < 0)
            score = 0;
        UpdateScoreValue(score);
        ShowPopup(origin, value, textColor);
    }
    
    public void UpdateScoreValue (int score)
    {
        scoreText.text = score.ToString();
        endGameScore.text = scoreText.text;
    }

    public void ShowPopup (Vector3 origin, int value, Color color)
    {
        Transform popupTransform = gameObject.transform;
        GameObject newPopup = Instantiate(popupTemplate, gameObject.transform);
        newPopup.transform.position = origin;
        newPopup.GetComponent<PopupController>().SetText(value.ToString(), color);
    }
}
