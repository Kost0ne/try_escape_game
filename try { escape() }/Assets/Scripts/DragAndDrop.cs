using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 lastNotNullVelocity;
    private TargetJoint2D targetJoint;
    private int[] Layers;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Layers = new[]
        {
            LayerMask.GetMask("Default"),
            LayerMask.GetMask("BackCollision"),
            LayerMask.GetMask("2BackCollision"),
            LayerMask.GetMask("Card")
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckClick(out var mousePos))
        {
            //var objectPos = transform.InverseTransformPoint(mousePos);
            targetJoint = gameObject.AddComponent<TargetJoint2D>();
            targetJoint.dampingRatio = 1.0f;
            targetJoint.frequency = 5.0f;
            var t = targetJoint.transform.InverseTransformPoint(mousePos);
            print(t);
            targetJoint.anchor = t;
        }

        if (targetJoint)
        {
            targetJoint.target = mousePos;
            Debug.DrawLine(targetJoint.transform.TransformPoint(targetJoint.anchor), mousePos);
        }

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }


    private bool CheckClick(out Vector2 mousePos)
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(targetJoint);
            targetJoint = null;
            return false;
        }

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useDepth = true;


        if (Input.GetMouseButtonDown(0))
        {
            // var hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //
            //
            // if (hit.collider == null) return false;
            //
            // if (hit.collider.gameObject == gameObject)
            // {
            //     return true;
            // }

            foreach (var layer in Layers)
            {
                var collider = Physics2D.OverlapPoint(mousePos, layer);
                if (collider != null && collider.attachedRigidbody.gameObject == gameObject) return true;
            }
        }

        return false;
    }
}