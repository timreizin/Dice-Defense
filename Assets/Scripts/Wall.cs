using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    int health = 3;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DecreaseHP()
    {
        --health;
        return health > 0;
    }

}
