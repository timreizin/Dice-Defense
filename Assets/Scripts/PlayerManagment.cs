using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    public int leftFace;
    public int rightFace;
    public int topFace;
    public int bottomFace;
    public int upFace;    
    public int downFace;
    public int health;
    void Start()
    {
        
    }

    void Update()
    {
        if(health <= 0)
        {
            //end the game, show score and ask for replay

        }
    }

    public void rotateDice(string mode)
    {
        //rotate values of dice faces

        int temp;
        if(mode == "left")
        {
            temp = topFace;
            topFace = rightFace;
            rightFace = bottomFace;
            bottomFace = leftFace;
            leftFace = temp;
        }
        if (mode == "right")
        {
            temp = topFace;
            topFace = leftFace;
            leftFace = bottomFace;
            bottomFace = rightFace;
            rightFace = temp;
        }
        if (mode == "up")
        {
            temp = topFace;
            topFace = downFace;
            downFace = bottomFace;
            bottomFace = upFace;
            upFace = temp;
        }
        if (mode == "down")
        {
            temp = topFace;
            topFace = upFace;
            upFace = bottomFace;
            bottomFace = downFace;
            downFace = temp;
        }
    }
}
