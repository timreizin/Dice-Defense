using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    private string text;
    public TMP_Text outputText;

    // Update is called once per frame
    void Update()
    {
        text = "Money: " + GlobalGameData.money.ToString();
        outputText.text = text;
    }
}
