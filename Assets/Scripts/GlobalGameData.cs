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

    public const int HORIZONTAL_SIZE = 15;
    public const int VERTICAL_SIZE = 15;

    public const int CELL_SIZE = 60;

    public static GameObject[,] objectsTable = new GameObject[HORIZONTAL_SIZE, VERTICAL_SIZE]; //down left corner is (0, 0), like in usual cartesian coordinates
}
