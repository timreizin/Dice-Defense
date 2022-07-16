using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameData 
{
    //global game info, availble for every object

    public static float cellUpdateTime;
    public static bool IsTabMenuOpened = false;
    public static bool IsGamePaused = false;
    public static string gamePhase = "enemySpawnTurn";
    public static int score = 0;
    public static int money = 0;
}
