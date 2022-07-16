using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MOVE_TIME = 1; //time in which enemy moves from square to square

    public int type;

    public GameObject gameLogic;

    // Start is called before the first frame update
    void Start()
    {

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

    //add function for destroyment

}
