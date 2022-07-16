using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManagment : MonoBehaviour
{
    private float timer;
    private int previousDiceTopFace;
    private string lastDiceRotation;
    public PlayerManagment playerManagment;

    private void Start()
    {
        playerManagment = GameObject.Find("Player").GetComponent<PlayerManagment>();
    }

    void Update()
    {
        if (!GlobalGameData.IsGamePaused)
        {
            //update local timer
            timer += Time.deltaTime;

            //check all turns situations
            if (GlobalGameData.gamePhase == "enemySpawnTurn")
            {
                //spawn new enemies
                int spawnRate = getSpawnRate();
                for(int i = 0; i < spawnRate; i++)
                {
                    //choose position
                    Vector2 spawnPoint = getRandomPosition();
                    //spawn
                }
            }
            if (GlobalGameData.gamePhase == "playerTurn")
            {
                //check for main cube rotation


                //Important! rotation goes immidiately, fix this plz


                if (Input.GetKeyDown(KeyCode.W))
                {
                    //move up
                    previousDiceTopFace = playerManagment.topFace;
                    lastDiceRotation = "up";
                    playerManagment.rotateDice("up");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //move left
                    previousDiceTopFace = playerManagment.topFace;
                    lastDiceRotation = "left";
                    playerManagment.rotateDice("left");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //move down
                    previousDiceTopFace = playerManagment.topFace;
                    lastDiceRotation = "down";
                    playerManagment.rotateDice("down");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //move right
                    previousDiceTopFace = playerManagment.topFace;
                    lastDiceRotation = "right";
                    playerManagment.rotateDice("right");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
            }
            if (GlobalGameData.gamePhase == "enemyFirstTurn")
            {
                //move every target(previous cube state) enemy for the first time
            }
            if (GlobalGameData.gamePhase == "turrelShooting")
            {
                //shoot from every turrel
            }
            if (GlobalGameData.gamePhase == "enemySecondTurn")
            {
                //move every target(new cube state) enemy for the second time
            }
            if(GlobalGameData.gamePhase == "endOfCycleTurn")
            {
                //do all stuff at the end of full turns cycle

                GlobalGameData.score += getScoreIncome();
                GlobalGameData.money += getMoneyIncome();
                GlobalGameData.gamePhase = "enemySpawnTurn";
            }
        }
    }

    int getSpawnRate()
    {
        //get number of enemies to spawn

        //temporary!
        return 3;
    }

    Vector2 getRandomPosition()
    {
        //get random spawn position(in cell notation)

        Vector2 spawnPoint;
        int randPos = Random.Range(0, 56);
        if (randPos < 15)
        {
            spawnPoint = new Vector2(randPos, 0);
        }
        else if (randPos < 29)
        {
            spawnPoint = new Vector2(14, randPos - 14);
        }
        else if (randPos < 43)
        {
            spawnPoint = new Vector2(42 - randPos, 14);
        }
        else
        {
            spawnPoint = new Vector2(0, 56 - randPos);
        }
        return spawnPoint;
    }

    int getMoneyIncome()
    {
        //get money income

        //temporary!
        return 1;
    }

    int getScoreIncome()
    {
        //get score income

        //temporary!
        return 1;
    }
}
