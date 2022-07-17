using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesTextManagment : MonoBehaviour
{
    private string text;
    public TMP_Text outputText;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        text = "Lives: " + player.GetComponent<PlayerManagment>().health.ToString();
        outputText.text = text;
    }
}
