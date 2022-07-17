using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject gameLogic;

    public float speed = 1.5f;

    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1, 1, 0) * speed * Time.deltaTime;
        transform.position = startingPosition;
        if (transform.localScale.x >= 4)
        {
            gameLogic.GetComponent<TurnsManagment>().counter--;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag != "Enemy") return;
        Destroy(collider.gameObject);
        GetComponentInParent<Turret>().EnemyKilled();
    }
}
