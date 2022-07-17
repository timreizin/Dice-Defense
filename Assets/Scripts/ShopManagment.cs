using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagment : MonoBehaviour
{
    public int minAllowedDistance;
    public int maxAllowedDistance;
    public GameObject building;
    public GameObject gameLogic;
    public int cost;
    public int turretType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalGameData.gamePhase == "playerTurn" && !GlobalGameData.IsGamePaused && !GlobalGameData.IsTabMenuOpened)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GlobalGameData.selectedBuilding = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (building == GlobalGameData.selectedBuilding)
                {
                    if (GlobalGameData.selectedBuilding.tag == "Effect")
                    {
                        Vector2 mousePos = Input.mousePosition;
                        int x = (int)((mousePos.x - 0.01f) / 60f);
                        int y = (int)((mousePos.y - 0.01f) / 60f);
                        if (Mathf.Max(Mathf.Abs(7 - x), Mathf.Abs(7 - y)) <= maxAllowedDistance &&
                            Mathf.Max(Mathf.Abs(7 - x), Mathf.Abs(7 - y)) >= minAllowedDistance)
                        {
                            gameLogic.GetComponent<EnemiesActions>().PushBack();
                            if (GlobalGameData.money >= cost)
                            {
                                GlobalGameData.money -= cost;
                            }
                        }
                    }
                    else
                    {
                        Vector2 mousePos = Input.mousePosition;
                        int x = (int)((mousePos.x - 0.01f) / 60f);
                        int y = (int)((mousePos.y - 0.01f) / 60f);
                        if (Mathf.Max(Mathf.Abs(7 - x), Mathf.Abs(7 - y)) <= maxAllowedDistance &&
                            Mathf.Max(Mathf.Abs(7 - x), Mathf.Abs(7 - y)) >= minAllowedDistance &&
                            GlobalGameData.money >= cost && GlobalGameData.objectsTable[x, y] == null)
                        {
                            GlobalGameData.money -= cost;
                            GlobalGameData.objectsTable[x, y] = Instantiate(building);
                            GlobalGameData.objectsTable[x, y].transform.position = FromTableToWorld(new Vector2Int(x, y));
                            if(GlobalGameData.objectsTable[x, y].tag == "Turret")
                            {
                                GlobalGameData.objectsTable[x, y].GetComponent<Turret>().gameLogic = gameLogic;
                                GlobalGameData.objectsTable[x, y].GetComponent<Turret>().type = turretType;
                            }
                        }
                    }
                }
            }
        }
        else GlobalGameData.selectedBuilding = null;
    }

    private void OnMouseDown()
    {
        if (GlobalGameData.gamePhase == "playerTurn" && !GlobalGameData.IsGamePaused && !GlobalGameData.IsTabMenuOpened)
        {
            GlobalGameData.selectedBuilding = building;
        }
    }
    Vector3 FromTableToWorld(Vector2Int position)
    {
        return new Vector3((30.5f + 60f * position.x) / 100f - (1201f / 200f), (30.5f + 60f * position.y) / 100f - (902f / 200f));
    }
}
