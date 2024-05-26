using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;



public class MenuControlling : MonoBehaviour
{
    public Button StartButton, OptionButton, QuitButton;
    public Button ControlsBackButton;
    public GameObject StartMenu, ControlsPanel;
    public PlayableDirector MainMenuEnter ,MainMenuLeave, ControlsEnter, ControlsLeave;


    // Start is called before the first frame update
    void Start()
    {
        MainMenuEnter.Play();
        MainMenuLeave.Stop();
        ControlsPanel.SetActive(false);
        //MainMenu Buttons
        StartButton.onClick.AddListener(StartButtonPressed);
        OptionButton.onClick.AddListener(OptionButtonPressed);
        QuitButton.onClick.AddListener(QuitButtonPressed);
        //Controls Buttons
        ControlsBackButton.onClick.AddListener(ControlsBackButtonPressed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void StartButtonPressed()
    {
        //Play game
        MainMenuEnter.Stop();
        MainMenuLeave.Play();
        StartCoroutine(StartGamePlay());
    }
    void OptionButtonPressed()
    {

        MainMenuEnter.Stop();
        MainMenuLeave.Play();
        ControlsPanel.SetActive(true);
        ControlsEnter.Play();
    }
    void QuitButtonPressed() { Application.Quit(); }

    void ControlsBackButtonPressed()
    {
        ControlsLeave.Play();
        StartCoroutine(ControlPanelStop());
        MainMenuEnter.Play();
    }

    IEnumerator StartGamePlay()
    {
        yield return new WaitForSeconds(1.65f);
        SceneManager.LoadScene(1);
    }

    IEnumerator ControlPanelStop() {
        yield return new WaitForSeconds(1.7f);
        ControlsPanel.SetActive(false); 
    }
}
