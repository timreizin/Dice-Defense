using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManagment : MonoBehaviour
{
    public GameObject backgroundArrows;
    // Start is called before the first frame update
    void Start()
    {
        backgroundArrows.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //check for tab menu

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            backgroundArrows.SetActive(true);
            GlobalGameData.IsTabMenuOpened = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            backgroundArrows.SetActive(false);
            GlobalGameData.IsTabMenuOpened = false;
        }

        //check for game pause
        {

        }
    }
}
