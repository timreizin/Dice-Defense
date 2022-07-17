using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject gameLogic;
    public Vector3 range;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic.GetComponent<TurnsManagment>().counter++;
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalGameData.IsTabMenuOpened)
        {
            transform.localScale = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        if (GlobalGameData.IsGamePaused) return;
        transform.position += transform.up * Time.deltaTime * speed;
        range.x -= Mathf.Abs(transform.up.x) * Time.deltaTime * speed;
        range.y -= Mathf.Abs(transform.up.y) * Time.deltaTime * speed;
        if (range.x < 0 || range.y < 0)
        {
            gameLogic.GetComponent<TurnsManagment>().counter--;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag != "Enemy") return;
        Destroy(collider.gameObject);
    }
}
