using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    void OnMouseOver()
    {
        Debug.Log("Mouse is over");
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("OnMouseOver");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }
    
    private void CheckClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider == null) return;

        if (hit.collider.gameObject == gameObject) Debug.Log("Raycast");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }

    void Update()
    {
        CheckClick();
    }
}