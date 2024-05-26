using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This script should be added to the game manager gameobject for convinience
public class StopwatchScript : MonoBehaviour
{
    //Declaring Variables

    // Public
    public bool isStopwatchOn = false;  // Determines if the stopwatch is on or not
    public float stopwatchTime = 0;    // Stores the value of how much time has passed

    // Private
    private TMP_Text stopwatchText;     // References the text component in the scene

    void Start()
    {
        GameObject stopwatchRef = GameObject.FindWithTag("Stopwatch");
        stopwatchText = stopwatchRef.GetComponent<TMP_Text>();

        // Resets stopwatch value to 0
        ResetStopwatch();

        // Temp start game
        StartGame();
    }
    void Update()
    {
        // If the stopwatch is on, time is increased and updated to the display in minutes and seconds
        if (isStopwatchOn)
        {
            stopwatchTime += Time.deltaTime;

            float minutes = Mathf.FloorToInt(stopwatchTime / 60);
            float seconds = Mathf.FloorToInt(stopwatchTime % 60);

            stopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // This function is called when the game starts (This should be a game manager function so this is temp)
    void StartGame()
    {
        StartStopwatch();
    }

    // This function starts the stopwatch
    void StartStopwatch()
    {
        isStopwatchOn = true;
    }

    // This function stops the stopwatch
    void EndStopwatch()
    {
        isStopwatchOn = false;
    }

    // This function resets the stopwatch
    void ResetStopwatch()
    {
        stopwatchTime = 0;
    }
}
