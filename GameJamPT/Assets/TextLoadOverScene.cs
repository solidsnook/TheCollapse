using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextLoadOverScene : MonoBehaviour
{
    public static TextLoadOverScene blackHoleScene;
    [SerializeField]
    public static GameObject textManager;
    
    public TMP_Text TimerTMPText;
    
    public string BlocksDestroyedTxt;

    public string TimerTxt;

    public BlackHoleScript bhs;

    private void Awake()
    {
        if (blackHoleScene == null)
        {
            blackHoleScene = this;
            textManager = this.gameObject;
           
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (bhs == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            bhs = GameObject.Find("BlackholeTest").GetComponent<BlackHoleScript>();
        }

        if (TimerTMPText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            TimerTMPText = GameObject.Find("Stopwatch").GetComponent<TMP_Text>();
        }
    }

    public void SetTimerText()
    {
        TimerTxt = TimerTMPText.text;
    }

    public void SetBlocksDestroyedText()
    {
        BlocksDestroyedTxt = bhs.blocksConsumed.ToString();
    }

}
