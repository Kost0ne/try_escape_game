using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject[] objects;

    private Rigidbody2D rigidBody;
    private Vector2 lastNotNullVelocity;
    private TargetJoint2D targetJoint;
    private int[] Layers;

    // Start is called before the first frame update
    void Start()
    {
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
        var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var obj = CheckClick(mousePos);

        if (obj != null)
        {
            targetJoint = obj.AddComponent<TargetJoint2D>();
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
    }


    GameObject CheckClick(Vector2 mousePos)
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(targetJoint);
            targetJoint = null;
        }


        if (Input.GetMouseButtonDown(0))
        {
            foreach (var layer in Layers)
            {
                var collider = Physics2D.OverlapPoint(mousePos, layer);
                if (collider != null) return collider.attachedRigidbody.gameObject;
            }
        }

        return null;
    }
}