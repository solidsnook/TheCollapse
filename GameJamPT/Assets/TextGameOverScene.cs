using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGameOverScene : MonoBehaviour
{
    public TextMeshProUGUI TimeSurvived;
    public TextMeshProUGUI BlocksConsumed;


    public void Awake()
    {
        TimeSurvived.text = "You Survived for: " + TextLoadOverScene.blackHoleScene.TimerTxt;
        BlocksConsumed.text = "Blocks Consumed:" + TextLoadOverScene.blackHoleScene.BlocksDestroyedTxt;
    }
}
