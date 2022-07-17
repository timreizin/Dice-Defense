using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    int health = 3;
    public Sprite[] sprite = new Sprite[3];

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[2];
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = sprite[health - 1];
    }

    public bool DecreaseHP()
    {
        --health;
        return health > 0;
    }

}
