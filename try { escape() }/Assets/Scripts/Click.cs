using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    private bool canClick = false;
    public GameObject objPopup;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick)
            Instantiate(objPopup);
    }
    
    public void OnTriggerStay2D(Collider2D colussion)
    { 
        canClick = true;
    }
    
    public void OnTriggerExit2D(Collider2D colussion)
    { 
        canClick = false;
    }
}
