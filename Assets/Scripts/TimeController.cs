using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float maxTimer = 10f;
    public TextMeshProUGUI timerText;

    public GameObject endGameScreen;

    float currTime;

    private void Awake ()
    {
        currTime = maxTimer; 
        UpdateTimerValue(currTime);
    }

    private void Update ()
    {
        if (currTime > 0f)
        {
            currTime -= Time.deltaTime;
            UpdateTimerValue(currTime);
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimerValue (float value)
    {
        int valueInt = Mathf.RoundToInt(value);
        timerText.text = valueInt.ToString();
    }

    public void EndGame ()
    {
        Time.timeScale = 0f;
        endGameScreen.SetActive(true);

    }
}
