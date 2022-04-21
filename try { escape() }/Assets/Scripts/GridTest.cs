using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    private Grid grid;
    public int width;
    public int height;
    public float cellSize;
    public Vector3 position;
    public GameObject wire;

    void Start()
    {
        grid = new Grid(width, height, cellSize, position);
        grid.VisualizeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        var position = GetWorldPosition();
        var (x, y) = grid.GetXY(position);
        if (Input.GetMouseButton(1))
        {
            if (grid.IsEmpty(x, y))
            {
                var clone = Instantiate(wire, 
                    grid.GetWorldPosition(x, y) + new Vector3(cellSize / 2, cellSize / 2),
                    Quaternion.identity);
                grid.SetValue(position, clone);
            }
        }
        if (Input.GetMouseButton(0))
        {
            grid.Delete(x, y);
        }
    }

    private Vector3 GetWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
}
