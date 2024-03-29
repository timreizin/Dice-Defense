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

    void CheckAndMove(Vector2Int from, Vector2Int move, int type = 0)
    {
        if (GlobalGameData.objectsTable[from.x, from.y] == null) return;
        if (GlobalGameData.objectsTable[from.x, from.y].tag != "Enemy") return;
        if (type != 0 && GlobalGameData.objectsTable[from.x, from.y].GetComponent<Enemy>().type != type) return;
        Vector2Int newPosition = from + move;
        if (!IsValidMove(newPosition))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalf(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f,
                                  GlobalGameData.CELL_SIZE * move.y / 100f,
                                  0)));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Turret"))
        {
            GetComponent<TurnsManagment>().counter++;
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveToDestroy(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f,
                                  GlobalGameData.CELL_SIZE * move.y / 100f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[newPosition.x, newPosition.y] = GlobalGameData.objectsTable[from.x, from.y];
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Player"))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfToPlayer(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f, GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Wall"))
        {
            //maybe add animation going half-way, and then back
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfAndBack(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
        }
        else if (IsObjectByTag(newPosition, "Mine"))
        {
            //add animation of moving there, and then baaaam
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfAndDestroy(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Enemy"))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalf(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0)));
            GlobalGameData.objectsTable[from.x, from.y] = null;
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
    } 

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
                GetComponent<TurnsManagment>().counter++;
                GlobalGameData.objectsTable[position.x, position.y] = Instantiate(Enemy);
                GlobalGameData.objectsTable[position.x, position.y].GetComponent<Enemy>().type = GetEnemyType();
                GlobalGameData.objectsTable[position.x, position.y].GetComponent<Enemy>().gameLogic = gameObject;
                if (position.x == 0)
                {
                    --position.x;
                    GlobalGameData.objectsTable[position.x + 1, position.y].transform.position = FromTableToWorld(position);
                    Vector2Int move = DirectionToMove('R');
                    StartCoroutine(GlobalGameData.objectsTable[position.x + 1, position.y].GetComponent<Enemy>().Move(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f, GlobalGameData.CELL_SIZE * move.y / 100f, 0)));
                }
                else if (position.x == GlobalGameData.HORIZONTAL_SIZE - 1)
                {
                    ++position.x;
                    GlobalGameData.objectsTable[position.x - 1, position.y].transform.position = FromTableToWorld(position);
                    Vector2Int move = DirectionToMove('L');
                    StartCoroutine(GlobalGameData.objectsTable[position.x - 1, position.y].GetComponent<Enemy>().Move(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f, GlobalGameData.CELL_SIZE * move.y / 100f, 0)));
                }
                else if (position.y == 0)
                {
                    --position.y;
                    GlobalGameData.objectsTable[position.x, position.y + 1].transform.position = FromTableToWorld(position);
                    Vector2Int move = DirectionToMove('U');
                    StartCoroutine(GlobalGameData.objectsTable[position.x, position.y + 1].GetComponent<Enemy>().Move(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f, GlobalGameData.CELL_SIZE * move.y / 100f, 0)));
                }
                else if (position.y == GlobalGameData.VERTICAL_SIZE - 1)
                {
                    ++position.y;
                    GlobalGameData.objectsTable[position.x, position.y - 1].transform.position = FromTableToWorld(position);
                    Vector2Int move = DirectionToMove('D');
                    StartCoroutine(GlobalGameData.objectsTable[position.x, position.y - 1].GetComponent<Enemy>().Move(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f, GlobalGameData.CELL_SIZE * move.y / 100f, 0)));
                }
            }
        }
    }

    char OppositeDirection(char direction)
    {
        switch (direction)
        {
            case 'U':
                return 'D';

            case 'D':
                return 'U';

            case 'R':
                return 'L';

            case 'L':
                return 'R';
        }
        return 'O';
    }

    public void PushBack()
    {
        for (int level = GlobalGameData.HORIZONTAL_SIZE / 2; level >= 1; --level)
        {
            Vector2Int iterator = new Vector2Int(GlobalGameData.HORIZONTAL_SIZE / 2 - level, GlobalGameData.VERTICAL_SIZE / 2 - level);
            while (++iterator.y != GlobalGameData.VERTICAL_SIZE / 2 + level)
            {
                CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            }

            while (++iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 + level)
            {
                CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            }

            while (--iterator.y != GlobalGameData.VERTICAL_SIZE / 2 - level)
            {
                CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            }

            while (--iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 - level)
            {
                CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            }

            //now corners
            CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            iterator.y += 2 * level;
            CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            iterator.x += 2 * level;
            CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
            iterator.y -= 2 * level;
            CheckAndMove(iterator, DirectionToMove(OppositeDirection(movementTable[iterator.x, iterator.y])));
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
    public void MoveTwoEnemiesToPlayer(int type1, int type2)
    {
        for (int level = 1; level <= GlobalGameData.HORIZONTAL_SIZE / 2; ++level)
        {
            Vector2Int iterator = new Vector2Int(GlobalGameData.HORIZONTAL_SIZE / 2 - level, GlobalGameData.VERTICAL_SIZE / 2 - level);
            while (++iterator.y != GlobalGameData.VERTICAL_SIZE / 2 + level)
            {
                CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            }

            while (++iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 + level)
            {
                CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            }

            while (--iterator.y != GlobalGameData.VERTICAL_SIZE / 2 - level)
            {
                CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            }

            while (--iterator.x != GlobalGameData.HORIZONTAL_SIZE / 2 - level)
            {
                CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            }

            //now corners
            CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            iterator.y += 2 * level;
            CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            iterator.x += 2 * level;
            CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
            iterator.y -= 2 * level;
            CheckAndMove2(iterator, DirectionToMove(movementTable[iterator.x, iterator.y]), type1, type2);
        }
    }
    void CheckAndMove2(Vector2Int from, Vector2Int move, int type1, int type2)
    {
        if (GlobalGameData.objectsTable[from.x, from.y] == null) return;
        if (GlobalGameData.objectsTable[from.x, from.y].tag != "Enemy") return;
        if (GlobalGameData.objectsTable[from.x, from.y].GetComponent<Enemy>().type != type1 &&
            GlobalGameData.objectsTable[from.x, from.y].GetComponent<Enemy>().type != type2) return;
        Vector2Int newPosition = from + move;
        if (!IsValidMove(newPosition))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalf(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f,
                                  GlobalGameData.CELL_SIZE * move.y / 100f,
                                  0)));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Turret"))
        {
            GetComponent<TurnsManagment>().counter++;
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveToDestroy(new Vector3(GlobalGameData.CELL_SIZE * move.x / 100f,
                                  GlobalGameData.CELL_SIZE * move.y / 100f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[newPosition.x, newPosition.y] = GlobalGameData.objectsTable[from.x, from.y];
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Player"))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfToPlayer(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f, GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Wall"))
        {
            //maybe add animation going half-way, and then back
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfAndBack(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
        }
        else if (IsObjectByTag(newPosition, "Mine"))
        {
            //add animation of moving there, and then baaaam
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalfAndDestroy(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0), GlobalGameData.objectsTable[newPosition.x, newPosition.y]));
            GlobalGameData.objectsTable[from.x, from.y] = null;
        }
        else if (IsObjectByTag(newPosition, "Enemy"))
        {
            StartCoroutine(GlobalGameData.objectsTable[from.x, from.y]
                .GetComponent<Enemy>()
                .MoveHalf(new Vector3(GlobalGameData.CELL_SIZE * move.x / 200f,
                                  GlobalGameData.CELL_SIZE * move.y / 200f,
                                  0)));
            GlobalGameData.objectsTable[from.x, from.y] = null;
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
}
