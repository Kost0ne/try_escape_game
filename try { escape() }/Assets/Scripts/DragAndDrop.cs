using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private bool dragging;
    private float EPS = 0.01f;
    private Rigidbody2D rigidBody;
    private bool onPosition;
    private Vector2 lastNotNullVelocity;
    private Vector2 objectPos;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // var t = Vector2.up;
        // var u = transform.TransformPoint(new Vector2(0, 0));
        // print(u.x.ToString() + " " + u.y.ToString() + " " + u.z.ToString());
        // rigidBody.AddForceAtPosition(t * 3, u);
        //rigidBody.AddForce(Vector2.up);
        CheckClick();
        var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = (Vector2) transform.TransformPoint(objectPos);
        if (dragging && !onPosition)
        {
            var dir = mousePos - position;
            lastNotNullVelocity = dir.normalized * Vector2.Distance(mousePos, position) * 25;
            
            var t = dir.normalized * Mathf.Pow(Vector2.Distance(mousePos, position) * 0.1f, 1);
            //rigidBody.AddForceAtPosition(t, position, ForceMode2D.Impulse);
            rigidBody.velocity = lastNotNullVelocity;
            
            
        }
        
        if (Vector2.Distance(mousePos, position) < EPS)
        {
            onPosition = true;
            rigidBody.velocity = Vector2.zero;
        }
        else
        {
            onPosition = false;
        }
    }


    private void CheckClick()
    {
        if (Input.GetMouseButtonUp(0) && dragging)
        {
            rigidBody.gravityScale = 1;
            rigidBody.velocity = lastNotNullVelocity;
            dragging = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;

            if (hit.collider.gameObject == gameObject)
            {
                //rigidBody.gravityScale = 0;
                dragging = true;
                objectPos = transform.InverseTransformPoint(mousePos);
                return ;
            }
        }
    }
}