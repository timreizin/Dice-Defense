using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalText : MonoBehaviour
{
    public TMP_Text text;
    // Update is called once per frame
    void Update()
    {
        string textString = "You died!\n";
        textString += "Score: ";
        textString += GlobalGameData.score.ToString();
        text.text = textString;
    }
}
