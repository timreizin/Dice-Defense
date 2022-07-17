using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagment : MonoBehaviour
{
    public DiceFace leftFace;
    public DiceFace rightFace;
    public DiceFace topFace;
    public DiceFace bottomFace;
    public DiceFace downFace;
    public DiceFace upFace;
    public int health;
    private SpriteRenderer spriteRenderer;
    public GameObject background;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        background.GetComponent<BackgroundManagment>().updateBackgroundPlayerFaces();
    }

    void Update()
    {

    }

    public void rotateDice(string mode)
    {
        //rotate values of dice faces

        DiceFace temp;

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
        spriteRenderer.sprite = topFace.sprite;
        background.GetComponent<BackgroundManagment>().updateBackgroundPlayerFaces();
    }

    public void DecreaseHP()
    {
        --health;
        if (health <= 0)
        {
            //end the game, show score and ask for replay
            SceneManager.LoadScene("TheEnd");
        }
    }
}
