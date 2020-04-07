using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
