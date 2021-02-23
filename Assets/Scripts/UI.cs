using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Game Time Display
    [SerializeField]
    Text timeDisplay;

    float currentTime = 0f;
    bool timerRunning;

    // Game Message display
    [SerializeField]
    Text gameMessage;

    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            currentTime += Time.deltaTime;

            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timeDisplay.text = string.Format("{0:00}:{1:00}", (int) time.TotalMinutes, (int) time.Seconds);
        }
    }

    public void GameOver()
    {
        timerRunning = false;

        gameMessage.text = "GAME OVER";
    }
}
