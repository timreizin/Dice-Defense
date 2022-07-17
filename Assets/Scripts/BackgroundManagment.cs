using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManagment : MonoBehaviour
{
    public GameObject backgroundArrows;
    public GameObject backgroundYourTurnLabel;
    public GameObject BackgroundRightFace;
    public GameObject BackgroundLeftFace;
    public GameObject BackgroundTopFace;
    public GameObject BackgroundUpFace;
    public GameObject BackgroundDownFace;
    public GameObject pause;
    public GameObject player;

    private void Awake()
    {
        GlobalGameData.DataRestart();
    }

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
        if (!GlobalGameData.IsGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                backgroundArrows.SetActive(true);
                GlobalGameData.IsTabMenuOpened = true;
                GlobalGameData.selectedBuilding = null;
                for (int i = 0; i < GlobalGameData.HORIZONTAL_SIZE; ++i)
                {
                    for (int j = 0; j < GlobalGameData.VERTICAL_SIZE; ++j)
                    {
                        if (i == 7 && j == 7) continue;
                        if (GlobalGameData.objectsTable[i, j] != null)
                        {
                            GlobalGameData.objectsTable[i, j].transform.localScale = new Vector3(0, 0, 0);
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                backgroundArrows.SetActive(false);
                GlobalGameData.IsTabMenuOpened = false;
                for (int i = 0; i < GlobalGameData.HORIZONTAL_SIZE; ++i)
                {
                    for (int j = 0; j < GlobalGameData.VERTICAL_SIZE; ++j)
                    {
                        if (i == 7 && j == 7) continue;
                        if (GlobalGameData.objectsTable[i, j] != null)
                        {
                            GlobalGameData.objectsTable[i, j].transform.localScale = new Vector3(1, 1, 1);
                        }
                    }
                }
            }
        }

        if(GlobalGameData.gamePhase == "playerTurn")
        {
            backgroundYourTurnLabel.SetActive(true);
        }
        else
        {
            backgroundYourTurnLabel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalGameData.IsGamePaused = !GlobalGameData.IsGamePaused;
            Debug.Log(GlobalGameData.IsGamePaused);
        }
        if (GlobalGameData.IsGamePaused)
        {
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
        }
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
