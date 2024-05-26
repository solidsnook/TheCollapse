using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameOverScript : MonoBehaviour
{
    // Declaring Variables

    // Public
    public bool isGameOver = false;

    // Private
    private TMP_Text blocksConsumedText;
    private TMP_Text timeSurvivedText;
    private BlackHoleScript blackHoleScript;
    private StopwatchScript stopwatchScript;

    //Button Controlling
    public Button RertyButton, QuitButton;


    // All this code can just be used in the game manager this is just temp
    void Start()
    {
        // Finds and References the BlocksConsumed UI
        GameObject blocksConsumedTextRef = GameObject.Find("BlocksConsumedText");
        blocksConsumedText = blocksConsumedTextRef.GetComponent<TMP_Text>();

        // Finds and References the TimeSurvived UI
        GameObject timeSurvivedTextRef = GameObject.Find("TimeSurvivedText");
        timeSurvivedText = timeSurvivedTextRef.GetComponent<TMP_Text>();

        // Finds and References the Black Hole Script
        GameObject blackHoleRef = GameObject.FindGameObjectWithTag("BlackHole");
        blackHoleScript = blackHoleRef.GetComponent<BlackHoleScript>();

        // Finds and References the Stopwatch Script
        GameObject gameManagerRef = GameObject.Find("GameManager");
        stopwatchScript = gameManagerRef.GetComponent<StopwatchScript>();


        //Button Handling
        //RertyButton.onClick.AddListener(RestartGame);
        //QuitButton.onClick.AddListener(QuitGame);



    }

    void Update()
    {
        // If the game manager calls gameover the UI is updates using values from black hole & stopwatch script (This is temp)
        /*if (isGameOver) 
        {
            blocksConsumedText.text = blackHoleScript.blocksConsumed.ToString();
            timeSurvivedText.text = stopwatchScript.stopwatchTime.ToString();
        }*/
    }


    public void RestartGame()
    {
        Debug.Log("RestartGame");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

}
