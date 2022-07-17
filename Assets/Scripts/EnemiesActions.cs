using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesActions : MonoBehaviour
{
    public GameObject Enemy;

    GameObject player;

    char[,] movementTable = new char[GlobalGameData.HORIZONTAL_SIZE, GlobalGameData.VERTICAL_SIZE];

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];

        movementTable[GlobalGameData.HORIZONTAL_SIZE / 2, GlobalGameData.VERTICAL_SIZE / 2] = 'O';
        for (int level = 0; level < GlobalGameData.HORIZONTAL_SIZE - 1 - level; ++level)
        {
            for (int i = level; i < GlobalGameData.HORIZONTAL_SIZE - 1 - level; ++i)
            {
                movementTable[i, level] = 'U';
            }
        }
        for (int level = 0; level + 1 < GlobalGameData.HORIZONTAL_SIZE - level; ++level)
        {
            for (int i = 1 + level; i < GlobalGameData.HORIZONTAL_SIZE - level; ++i)
            {
                movementTable[i, GlobalGameData.VERTICAL_SIZE - level - 1] = 'D';
            }
        }
        for (int level = 0; level + 1 < GlobalGameData.VERTICAL_SIZE - level; ++level)
        {
            for (int i = 1 + level; i < GlobalGameData.VERTICAL_SIZE - level; ++i)
            {
                movementTable[level, i] = 'R';
            }
        }
        for (int level = 0; level < GlobalGameData.VERTICAL_SIZE - 1 - level; ++level)
        {
            for (int i = level; i < GlobalGameData.VERTICAL_SIZE - 1 - level; ++i)
            {
                movementTable[GlobalGameData.HORIZONTAL_SIZE - level - 1, i] = 'L';
            }
        }
        /*for (int i = GlobalGameData.VERTICAL_SIZE - 1; i >= 0; --i)
        {
            string s = "";
            for (int j = 0; j < GlobalGameData.HORIZONTAL_SIZE; ++j) s += movementTable[j, i];
            Debug.Log(s);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 check = new Vector2(2, 1);
        //Debug.Log(check[1]);
    }

    Vector2Int DirectionToMove(char direction)
    {
        switch (direction)
        {
            case 'L':
                return Vector2Int.left;

            case 'R':
                return Vector2Int.right;

            case 'U':
                return Vector2Int.up;

            case 'D':
                return Vector2Int.down;

            default:
                return Vector2Int.zero;
        }
    }

    bool IsValidMove(Vector2Int position)
    {
        if (position.x < 0 ||
            position.y < 0 ||
            position.x >= GlobalGameData.HORIZONTAL_SIZE ||
            position.y >= GlobalGameData.VERTICAL_SIZE)
            return false;
        //return GlobalGameData.objectsTable[position.x, position.y] == null; 
        return true;
    }

    bool IsObjectByTag(Vector2Int position, string tag)
    {
        if (position.x < 0 ||
            position.y < 0 ||
            position.x >= GlobalGameData.HORIZONTAL_SIZE ||
            position.y >= GlobalGameData.VERTICAL_SIZE)
            return false;
        if (GlobalGameData.objectsTable[position.x, position.y] == null) return false;
        return GlobalGameData.objectsTable[position.x, position.y].tag == tag;
    }

    Vector2Int StartingIterator(char direction)
    {
        switch (direction)
        {
            case 'L':
                return new Vector2Int(0, 0);

            case 'R':
                return new Vector2Int(GlobalGameData.HORIZONTAL_SIZE - 1, 0);

            case 'U':
                return new Vector2Int(0, GlobalGameData.VERTICAL_SIZE - 1);

            case 'D':
                return new Vector2Int(0, 0);

            default:
                return Vector2Int.zero;
        }
    }

    bool IncrementIterator(ref Vector2Int iterator, char direction)
    {
        switch (direction)
        {
            case 'L':
                ++iterator.y;
                if (iterator.y == GlobalGameData.VERTICAL_SIZE)
                {
                    ++iterator.x;
                    iterator.y = 0;
                }
                if (iterator.x == GlobalGameData.HORIZONTAL_SIZE) return false;
                break;

            case 'R':
                ++iterator.y;
                if (iterator.y == GlobalGameData.VERTICAL_SIZE)
                {
                    --iterator.x;
                    iterator.y = 0;
                }
                if (iterator.x == -1) return false;
                break;

            case 'D':
                ++iterator.x;
                if (iterator.x == GlobalGameData.HORIZONTAL_SIZE)
                {
                    ++iterator.y;
                    iterator.x = 0;
                }
                if (iterator.y == GlobalGameData.VERTICAL_SIZE) return false;
                break;

            case 'U':
                ++iterator.x;
                if (iterator.x == GlobalGameData.HORIZONTAL_SIZE)
                {
                    --iterator.y;
                    iterator.x = 0;
                }
                if (iterator.y == -1) return false;
                break;
        }
        return true;
    }

    void CheckAndMove(Vector2Int from, Vector2Int move, int type)
    {
        if (GlobalGameData.objectsTable[from.x, from.y] == null) return;
        if (GlobalGameData.objectsTable[from.x, from.y].tag != "Enemy") return;
        //Debug.Log(2);
        if (type != 0 && GlobalGameData.objectsTable[from.x, from.y].GetComponent<Enemy>().type != type) return;
        Vector2Int newPosition = from + move;
        if (!IsValidMove(newPosition))
        {
            //destroy
            Destroy(GlobalGameData.objectsTable[from.x, from.y]);
        }
        else if (IsObjectByTag(newPosition, "Player"))
        {
            player.GetComponent<PlayerManagment>().DecreaseHP();
            Destroy(GlobalGameData.objectsTable[from.x, from.y]);
        }
        else if (IsObjectByTag(newPosition, "Wall"))
        {
            //maybe add animation going half-way, and then back
            if (!GlobalGameData.objectsTable[newPosition.x, newPosition.y].GetComponent<Wall>().DecreaseHP())
            {
                Destroy(GlobalGameData.objectsTable[newPosition.x, newPosition.y]);
            }
        }
        else if (IsObjectByTag(newPosition, "Mine"))
        {
            //add animation of moving there, and then baaaam
            Destroy(GlobalGameData.objectsTable[newPosition.x, newPosition.y]);
            Destroy(GlobalGameData.objectsTable[from.x, from.y]);
        }
        else if (IsObjectByTag(newPosition, "Enemy"))
        {
            Destroy(GlobalGameData.objectsTable[from.x, from.y]);
        }
        else
        {
            GetComponent<TurnsManagment>().counter++;
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .Move(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f,
                                  GlobalGameData.CELL_SIZE * move.y / 100f,
                                  0)));
            GlobalGameData.objectsTable[newPosition.x, newPosition.y] = GlobalGameData.objectsTable[from.x, from.y];
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
    }

    public void MoveEnemies(int type, char direction)
    {
        Vector2Int move = DirectionToMove(direction);
        Vector2Int iterator = StartingIterator(direction);
        do
        {
            CheckAndMove(iterator, move, type);
        } while (IncrementIterator(ref iterator, direction));
    } // add case for movement into player

    public void MoveEnemiesToPlayer(int type)
    {
        for (int level = 1; level <= GlobalGameData.HORIZONTAL_SIZE / 2; ++level)
        {
            Vector2Int iterator = new Vector2Int(GlobalGameData.HORIZONTAL_SIZE / 2 - level, GlobalGameData.VERTICAL_SIZE / 2 - level);
            while (++iterator.y != GlobalGameData.VERTICAL_SIZE / 2 + level)
            {
                CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            }

            while (++iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 + level)
            {
                CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            }

            while (--iterator.y != GlobalGameData.VERTICAL_SIZE / 2 - level)
            {
                CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            }

            while (--iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 - level)
            {
                CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            }

            //now corners
            CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            iterator.y += 2 * level;
            CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            iterator.x += 2 * level;
            CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
            iterator.y -= 2 * level;
            CheckAndMove(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type);
        }
    }

    Vector2Int GetRandomPosition()
    {
        //get random spawn position(in cell notation)

        int randPos = Random.Range(0, 2 * (GlobalGameData.VERTICAL_SIZE + GlobalGameData.HORIZONTAL_SIZE) - 4);
        if (randPos < GlobalGameData.VERTICAL_SIZE)
        {
            return new Vector2Int(0, randPos);
        }
        else if (randPos < GlobalGameData.VERTICAL_SIZE + GlobalGameData.HORIZONTAL_SIZE - 1)
        {
            return new Vector2Int(randPos - GlobalGameData.VERTICAL_SIZE, GlobalGameData.VERTICAL_SIZE - 1);
        }
        else if (randPos < 2 * GlobalGameData.VERTICAL_SIZE + GlobalGameData.HORIZONTAL_SIZE - 2)
        {
            return new Vector2Int(GlobalGameData.HORIZONTAL_SIZE - 1, 2 * GlobalGameData.VERTICAL_SIZE + GlobalGameData.HORIZONTAL_SIZE - 3 - randPos);
        }
        else
        {
            return new Vector2Int(2 * (GlobalGameData.VERTICAL_SIZE + GlobalGameData.HORIZONTAL_SIZE) - 4 - randPos, 0);
        }
    }

    Vector3 FromTableToWorld(Vector2Int position)
    {
        return new Vector3((30.5f + 60f * position.x) / 100f - (1201f / 200f), (30.5f + 60f * position.y) / 100f - (902f / 200f));
    }

    int GetEnemyType()
    {
        int type = Random.Range(1, 7);
        return type;
    }

    public void SpawnEnemies(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            Vector2Int position = GetRandomPosition();
            if (GlobalGameData.objectsTable[position.x, position.y] == null)
            {
                //spawn
                GlobalGameData.objectsTable[position.x, position.y] = Instantiate(Enemy);
                GlobalGameData.objectsTable[position.x, position.y].transform.position = FromTableToWorld(position);
                GlobalGameData.objectsTable[position.x, position.y].GetComponent<Enemy>().type = GetEnemyType();
                GlobalGameData.objectsTable[position.x, position.y].GetComponent<Enemy>().gameLogic = gameObject;
            }
        }
    }

    /*public int countEnemies()
    {
        int amount = 0;
        for(int i = 0; i < GlobalGameData.HORIZONTAL_SIZE; ++i)
        {
            for(int j = 0; j < GlobalGameData.VERTICAL_SIZE; ++j)
            {
                if (GlobalGameData.objectsTable[i, j] != null && GlobalGameData.objectsTable[i, j].gameObject.tag == "Enemy") ++amount;
            }
        }
        return amount;
    }*/


}
