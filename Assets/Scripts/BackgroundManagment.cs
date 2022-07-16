using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManagment : MonoBehaviour
{
    public GameObject backgroundArrows;
    public GameObject backgroundYourTurnLabel;
    public GameObject BackgroundRightFace;
    public GameObject BackgroundLeftFace;
    public GameObject BackgroundTopFace;
    public GameObject BackgroundUpFace;
    public GameObject BackgroundDownFace;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        backgroundArrows.SetActive(false);
        backgroundYourTurnLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //check for tab menu

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            backgroundArrows.SetActive(true);
            GlobalGameData.IsTabMenuOpened = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            backgroundArrows.SetActive(false);
            GlobalGameData.IsTabMenuOpened = false;
        }

        //handle

        if(GlobalGameData.gamePhase == "playerTurn")
        {
            backgroundYourTurnLabel.SetActive(true);
        }
        else
        {
            backgroundYourTurnLabel.SetActive(false);
        }

        //check for game pause
        
    }
    public void updateBackgroundPlayerFaces()
    {
        BackgroundRightFace.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerManagment>().rightFace.sprite;
        BackgroundLeftFace.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerManagment>().leftFace.sprite;
        BackgroundUpFace.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerManagment>().upFace.sprite;
        BackgroundDownFace.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerManagment>().downFace.sprite;
        BackgroundTopFace.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerManagment>().topFace.sprite;
    }
}
