using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextManagment : MonoBehaviour
{
    private string text;
    public TMP_Text outputText;

    // Update is called once per frame
    void Update()
    {
        text = "money: " + GlobalGameData.money.ToString();
        outputText.text = text;
    }
}
