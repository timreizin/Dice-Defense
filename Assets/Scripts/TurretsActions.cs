using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        for (int i = 0; i < GlobalGameData.HORIZONTAL_SIZE; ++i)
        {
            for (int j = 0; j < GlobalGameData.VERTICAL_SIZE; ++j)
            {
                if (GlobalGameData.objectsTable[i, j] == null) continue;
                if (GlobalGameData.objectsTable[i, j].tag != "Turret") continue;
                GlobalGameData.objectsTable[i, j].GetComponent<Turret>().Shoot();
            }
        }
    }

    public void SpawnTurret(Vector2Int position)
    {

    }
}
