using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GButtons : MonoBehaviour
{
    public int inc;
    public GameObject Bg;

    private void OnMouseDown()
    {
        Bg.GetComponent<TutorBG>().page = Mathf.Max(0, Bg.GetComponent<TutorBG>().page + inc);
    }
}
