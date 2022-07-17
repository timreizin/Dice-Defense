using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MOVE_TIME = 0.5f; //time in which enemy moves from square to square

    public int type;
    public Sprite[] sprite = new Sprite[6];

    public GameObject gameLogic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[type - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Move(Vector3 move)
    {
        Vector3 position = transform.position;
        Vector3 newPosition = position + move;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / MOVE_TIME);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME) transform.position = newPosition;
            yield return null;
        }
        gameLogic.GetComponent<TurnsManagment>().counter--;
    }

    public IEnumerator MoveHalfAndBack(Vector3 move, GameObject wall)
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
        Vector3 position = transform.position;
        Vector3 newPosition = position + move / 2f;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME / 2f)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / (MOVE_TIME / 2f));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME / 2f) transform.position = newPosition;
            yield return null;
        }

        if (!wall.GetComponent<Wall>().DecreaseHP())
        {
            Destroy(wall);
        }
        while (elapsedTime < MOVE_TIME)
        {
            transform.position = Vector3.Lerp(newPosition, position, elapsedTime / (MOVE_TIME / 2f) - 1f);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME) transform.position = position;
            yield return null;
        }
        gameLogic.GetComponent<TurnsManagment>().counter--;
    }

    public IEnumerator MoveHalf(Vector3 move)
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
        Vector3 position = transform.position;
        Vector3 newPosition = position + move / 2f;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME / 2f)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / (MOVE_TIME / 2f));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME / 2f) transform.position = newPosition;
            yield return null;
        }
        Destroy(gameObject);
        gameLogic.GetComponent<TurnsManagment>().counter--;
    }

    public IEnumerator MoveHalfAndDestroy(Vector3 move, GameObject toDestrot)
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
        Vector3 position = transform.position;
        Vector3 newPosition = position + move / 2f;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME / 2f)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / (MOVE_TIME / 2f));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME / 2f) transform.position = newPosition;
            yield return null;
        }
        Destroy(gameObject);
        Destroy(toDestrot);
        gameLogic.GetComponent<TurnsManagment>().counter--;
    }

    public IEnumerator MoveHalfToPlayer(Vector3 move, GameObject player)
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
        Vector3 position = transform.position;
        Vector3 newPosition = position + move / 2f;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME / 2f)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / (MOVE_TIME / 2f));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME / 2f) transform.position = newPosition;
            yield return null;
        }
        Destroy(gameObject);
        player.GetComponent<PlayerManagment>().DecreaseHP();
        gameLogic.GetComponent<TurnsManagment>().counter--;
    }

    public IEnumerator MoveToDestroy(Vector3 move, GameObject toDestroy)
    {
        Vector3 position = transform.position;
        Vector3 newPosition = position + move;
        float elapsedTime = 0;
        while (elapsedTime < MOVE_TIME)
        {
            transform.position = Vector3.Lerp(position, newPosition, elapsedTime / MOVE_TIME);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= MOVE_TIME) transform.position = newPosition;
            yield return null;
        }
        Destroy(toDestroy);
        gameLogic.GetComponent<TurnsManagment>().counter--;
        
    }

    //add function for destroyment

}
