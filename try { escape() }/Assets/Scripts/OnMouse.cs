using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour
{
    private Outline Outline;
    // Start is called before the first frame update
    void Start()
    {
        Outline = gameObject.GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        Outline.enabled = true;
    }
    
    private void OnMouseExit()
    {
        Outline.enabled = false;
    }
}
