using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextManagment : MonoBehaviour
{
    private string text;
    public TMP_Text outputText;

    // Update is called once per frame
    void Update()
    {
        text = "Score: " + GlobalGameData.score.ToString();
        outputText.text = text;
    }
}
