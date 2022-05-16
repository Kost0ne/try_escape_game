using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private bool dragging;
    private bool touchingBound;
    private float EPS = 0.01f;
    public GameObject bound;
    private Vector3 lastDir;
    private Rigidbody2D rigidBody;
    private bool onPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();

        var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = (Vector2) transform.position;
        if (dragging && !onPosition)
        {
            var dir = (mousePos - position);
            lastDir = dir;

            var dist = Vector2.Distance(mousePos, position);

            transform.position = position + dir.normalized * Mathf.Min(dist, 0.12f);
            //rigidBody.AddForce(new Vector2((distX) * Math.Sign(-transform.position.x + mousePos.x), 0));


            //  var distX = Math.Abs(position.x - mousePos.x);
            //  var distY = Math.Abs(position.y - mousePos.y);
            //  var forceDirX = Math.Sign(-position.x + mousePos.x) * distX * distX;
            //  var forceDirY = Math.Sign(-position.y + mousePos.y) * distY * distY; 
            //
            // rigidBody.AddForceAtPosition(new Vector2(forceDirX, forceDirY), mousePos);


            //lastPos = transform.position;
        }

        if (Math.Abs(position.x - mousePos.x) < EPS && Math.Abs(position.y - mousePos.y) < EPS) onPosition = true;
        else
        {
            onPosition = false;
        }
    }


    private Vector2 ClipForce()
    {
        var lastDirForce = lastDir.magnitude;

        //if (lastDirForce < 0.01f) return Vector2.zero;
        
        //if (lastDirForce < 0.05) return 100 * lastDir.normalized;
        if (lastDirForce > 20) return 750 * lastDir.normalized;
        return lastDir * 200;
    }


    private void CheckClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!dragging) return;
            dragging = false;
            rigidBody.gravityScale = 1;

            rigidBody.AddForce(ClipForce());
            return;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }


        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider == null) return;

        if (hit.collider.gameObject == gameObject)
        {
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
            dragging = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject == bound)
        {
            // if (dragging)
            //     transform.position = lastPos;
            print("oulala");
            // lastPos = transform.position;
            // touchingBound = true;
            dragging = false;
        }
    }
}