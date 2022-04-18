using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DrawLine : MonoBehaviour
{
    private LineRenderer line;
    private bool isMousePressed;
    public List<Vector3> pointsList;
    private Vector3 mousePos;
    
    // Structure for line points
    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };
    //    -----------------------------------    
    void Awake ()
    {
        // Create line renderer component and set its property
        line = gameObject.AddComponent<LineRenderer>();
        Debug.Log(line);
        // line.material = new Material(Shader.Find("Particles/Additive"));
        line.SetVertexCount(0);
        line.SetWidth(0.1f, 0.1f);
        line.SetColors(Color.green, Color.green);
        line.useWorldSpace = true;    
        isMousePressed = false;
        pointsList = new List<Vector3> ();
        //        renderer.material.SetTextureOffset(
    }
    //    -----------------------------------    
    void Update ()
    {
        // If mouse button down, remove old line and set its color to green
        if (Input.GetMouseButtonDown (0)) {
            isMousePressed = true;
            line.SetVertexCount (0);
            pointsList.RemoveRange (0, pointsList.Count);
            line.SetColors (Color.green, Color.green);
        }
        if (Input.GetMouseButtonUp (0)) {
            isMousePressed = false;
        }
        // Drawing line when mouse is moving(presses)
        if (!isMousePressed) return;
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePos.z = 0;
        if (pointsList.Contains(mousePos)) return;
        pointsList.Add (mousePos);
        line.SetVertexCount (pointsList.Count);
        line.SetPosition (pointsList.Count - 1, (Vector3)pointsList [pointsList.Count - 1]);
        if (!isLineCollide()) return;
        isMousePressed = false;
        line.SetColors (Color.red, Color.red);
    }
    //    -----------------------------------    
    //  Following method checks is currentLine(line drawn by last two points) collided with line 
    //    -----------------------------------    
    private bool isLineCollide ()
    {
        if (pointsList.Count < 2)
            return false;
        var TotalLines = pointsList.Count - 1;
        var lines = new myLine[TotalLines];
        if (TotalLines > 1) {
            for (var i=0; i<TotalLines; i++) {
                lines [i].StartPoint = (Vector3)pointsList [i];
                lines [i].EndPoint = (Vector3)pointsList [i + 1];
            }
        }
        for (var i=0; i<TotalLines-1; i++) {
            myLine currentLine;
            currentLine.StartPoint = (Vector3)pointsList [pointsList.Count - 2];
            currentLine.EndPoint = (Vector3)pointsList [pointsList.Count - 1];
            if (IsLinesIntersect (lines [i], currentLine)) 
                return true;
        }
        return false;
    }
    //    -----------------------------------    
    //    Following method checks whether given two points are same or not
    //    -----------------------------------    
    private bool checkPoints (Vector3 pointA, Vector3 pointB)
    {
        return (pointA.x == pointB.x && pointA.y == pointB.y);
    }
    //    -----------------------------------    
    //    Following method checks whether given two line intersect or not
    //    -----------------------------------    
    private bool IsLinesIntersect (myLine l1, myLine l2)
    {
        if (checkPoints (l1.StartPoint, l2.StartPoint) ||
            checkPoints (l1.StartPoint, l2.EndPoint) ||
            checkPoints (l1.EndPoint, l2.StartPoint) ||
            checkPoints (l1.EndPoint, l2.EndPoint))
            return false;
        
        return Mathf.Max (l1.StartPoint.x, l1.EndPoint.x) >= Mathf.Min (l2.StartPoint.x, l2.EndPoint.x) &&
              Mathf.Max (l2.StartPoint.x, l2.EndPoint.x) >= Mathf.Min (l1.StartPoint.x, l1.EndPoint.x) &&
              Mathf.Max (l1.StartPoint.y, l1.EndPoint.y) >= Mathf.Min (l2.StartPoint.y, l2.EndPoint.y) &&
              Mathf.Max (l2.StartPoint.y, l2.EndPoint.y) >= Mathf.Min (l1.StartPoint.y, l1.EndPoint.y);
    }
}