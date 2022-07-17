using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public int type = 0;
    public int range = 2;
    public GameObject bullet;
    //public GameObject wave;

    public GameObject gameLogic;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector2Int position)
    {
        if (type == 0)
        {
            //bullets horizontal

            GameObject newBullet = Instantiate(bullet);
            newBullet.GetComponent<Bullet>().gameLogic = gameLogic;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, 90);//right
            newBullet.GetComponent<Bullet>().range = new Vector3(range * GlobalGameData.CELL_SIZE / 100f, range * GlobalGameData.CELL_SIZE / 100f, 0);
            newBullet.transform.position = FromTableToWorld(position);
            newBullet = Instantiate(bullet);
            newBullet.GetComponent<Bullet>().gameLogic = gameLogic;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, -90);//left
            newBullet.GetComponent<Bullet>().range = new Vector3(range * GlobalGameData.CELL_SIZE / 100f, range * GlobalGameData.CELL_SIZE / 100f, 0);
            newBullet.transform.position = FromTableToWorld(position);
        }
        else if (type == 1)
        {
            //bullets vertical
            GameObject newBullet = Instantiate(bullet);
            newBullet.GetComponent<Bullet>().gameLogic = gameLogic;
            newBullet.GetComponent<Bullet>().range = new Vector3(range * GlobalGameData.CELL_SIZE / 100f, range * GlobalGameData.CELL_SIZE / 100f, 0);
            newBullet.transform.position = FromTableToWorld(position);
            //up
            newBullet = Instantiate(bullet);
            newBullet.GetComponent<Bullet>().gameLogic = gameLogic;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, 180);
            newBullet.GetComponent<Bullet>().range = new Vector3(range * GlobalGameData.CELL_SIZE / 100f, range * GlobalGameData.CELL_SIZE / 100f, 0);
            newBullet.transform.position = FromTableToWorld(position);
            //down
        }
        else 
        {
            //waves
        }
    }
    Vector3 FromTableToWorld(Vector2Int position)
    {
        return new Vector3((30.5f + 60f * position.x) / 100f - (1201f / 200f), (30.5f + 60f * position.y) / 100f - (902f / 200f));
    }
}
