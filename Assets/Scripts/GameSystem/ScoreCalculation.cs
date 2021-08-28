using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    public int Score;
    private int ComboScore;

    private bool ComboActive;

    private int ComboMultiplyer;
    private int ComboKillCount;

    private float ComboTimerLimit;
    private float _timer;

    void Start()
    {
        Score = 0;
        ComboScore = 0;

        _timer = 0;

        ComboMultiplyer = 0;
        ComboTimerLimit = 6.00f;  
    }


    private void Update()
    {
        if (ComboActive == true)
        {
            _timer += Time.deltaTime;
        }

        ComboReset();

        MultiplyerChanges();
    }



    private void ComboReset()  // if timer is longer than specified amount, reset the combo and its attributes, and ofcourse add the combo points to score
    {
        if (_timer >= ComboTimerLimit)
        {
            Score += ComboScore;
            ComboKillCount = 0;
            ComboMultiplyer = 0;
            ComboScore = 0;
            ComboActive = false;
            _timer = 0;

            Debug.Log("combo ended, current total score is " + Score);
        }
    }
    private void MultiplyerChanges()   // ComboMultiplyer changes according to amount of pickups in 1 combo
    {
        if (ComboKillCount >= 1 && ComboKillCount <= 5)
        {
            ComboMultiplyer = 1;
        }
        else if (ComboKillCount >= 6 && ComboKillCount <= 10)
        {
            ComboMultiplyer = 2;
        }
        else if (ComboKillCount >= 11 && ComboKillCount <= 15)
        {
            ComboMultiplyer = 3;
        }
        else if (ComboKillCount >= 16 && ComboKillCount <= 20)
        {
            ComboMultiplyer = 4;
        }
        else if (ComboKillCount >= 21)
        {
            ComboMultiplyer = 5;
        }
    }

    public void IncreaseScore(int scoreValue)
    {
        Score += scoreValue;
        ComboScore += scoreValue * ComboMultiplyer;
        ComboActive = true;
        _timer = 0;    // timer reset
        ComboKillCount += 1;

        Debug.Log("this kill rewards you with " + scoreValue + "normal points and " + ComboScore + "combo points");
    }

}
