using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManagment : MonoBehaviour
{
    private float timer;
    private int previousDiceTopFace;
    private string lastDiceRotation;
    public GameObject player;

    void Start()
    {

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
                GetComponent<EnemiesActions>().SpawnEnemies(getSpawnRate());
                GlobalGameData.gamePhase = "playerTurn";
            }
            if (GlobalGameData.gamePhase == "playerTurn")
            {
                //check for main cube rotation


                //Important! rotation goes immidiately, fix this plz


                if (Input.GetKeyDown(KeyCode.W))
                {
                    //move up
                    previousDiceTopFace = player.GetComponent<PlayerManagment>().topFace.type;
                    lastDiceRotation = "up";
                    player.GetComponent<PlayerManagment>().rotateDice("up");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //move left
                    previousDiceTopFace = player.GetComponent<PlayerManagment>().topFace.type;
                    lastDiceRotation = "left";
                    player.GetComponent<PlayerManagment>().rotateDice("left");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //move down
                    previousDiceTopFace = player.GetComponent<PlayerManagment>().topFace.type;
                    lastDiceRotation = "down";
                    player.GetComponent<PlayerManagment>().rotateDice("down");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //move right
                    previousDiceTopFace = player.GetComponent<PlayerManagment>().topFace.type;
                    lastDiceRotation = "right";
                    player.GetComponent<PlayerManagment>().rotateDice("right");
                    GlobalGameData.gamePhase = "enemyFirstTurn";
                    return;
                }
            }
            if (GlobalGameData.gamePhase == "enemyFirstTurn")
            {
                //move every target(previous cube state) enemy for the first time
                char move = 'O';
                switch (lastDiceRotation)
                {
                    case "up":
                        move = 'U';
                        break;

                    case "down":
                        move = 'D';
                        break;

                    case "left":
                        move = 'L';
                        break;

                    case "right":
                        move = 'R';
                        break;
                }
                GetComponent<EnemiesActions>().MoveEnemies(previousDiceTopFace, move);
                GlobalGameData.gamePhase = "turrelShooting";
            }
            if (GlobalGameData.gamePhase == "turrelShooting")
            {
                //shoot from every turrel
                GlobalGameData.gamePhase = "enemySecondTurn";
            }
            if (GlobalGameData.gamePhase == "enemySecondTurn")
            {
                //move every target(new cube state) enemy for the second time
                GetComponent<EnemiesActions>().MoveEnemiesToPlayer(player.GetComponent<PlayerManagment>().topFace.type);
                GlobalGameData.gamePhase = "endOfCycleTurn";
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
