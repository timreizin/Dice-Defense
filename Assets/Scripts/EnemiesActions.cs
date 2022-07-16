using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesActions : MonoBehaviour
{
    char[,] movementTable = new char[GlobalGameData.HORIZONTAL_SIZE, GlobalGameData.VERTICAL_SIZE];

    void Start()
    {
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
        return GlobalGameData.objectsTable[position.x, position.y] == null; 
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

    public void MoveEnemies(int type, char direction)
    {
        Vector2Int move = DirectionToMove(direction);
        Vector2Int iterator = StartingIterator(direction);
        do
        {
            if (GlobalGameData.objectsTable[iterator.x, iterator.y] == null) continue;
            if (GlobalGameData.objectsTable[iterator.x, iterator.y].tag != "Enemy") continue;
            if (GlobalGameData.objectsTable[iterator.x, iterator.y].GetComponent<Enemy>().type != type) continue;
            Vector2Int newPosition = iterator + move;
            if (!IsValidMove(newPosition))
            {
                //destroy
                Destroy(GlobalGameData.objectsTable[iterator.x, iterator.y]);
            }
            else
            {
                GlobalGameData.objectsTable[iterator.x, iterator.y]
                    .GetComponent<Enemy>()
                    .Move(new Vector3(GlobalGameData.CELL_SIZE * move.x,
                                      GlobalGameData.CELL_SIZE * move.y,
                                      0));
                GlobalGameData.objectsTable[newPosition.x, newPosition.y] = GlobalGameData.objectsTable[iterator.x, iterator.y];
                GlobalGameData.objectsTable[iterator.x, iterator.y] = null;
            }
        } while (IncrementIterator(ref iterator, direction));
    }

    public void MoveEnemiesToPlayer(int type)
    {

    }

    public void SpawnEnemies(int amount)
    {

    }

}
