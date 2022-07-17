using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorBG : MonoBehaviour
{
    public int page = 0;
    public Sprite[] sprite = new Sprite[7];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            page = Mathf.Max(0, page - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            page++;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            page = 10;
        }
        if (page >= 7)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprite[page];
        }
    }
}
