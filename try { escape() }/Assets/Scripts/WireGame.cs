using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WireGame : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 position;
    private Dictionary<string, Stack<GameObject>> wires;
    private GameObject color;
    private Grid grid;

    private void SetUpLevel(GameObject[,] level)
    {
        for(var y = 0; y < width; y++)
        {
            for (var x = 0; x < height; x++)
            {
                if (level[x, y] == null) continue;
                var clone = GetClone(x, y, level[x, y]);
                clone.transform.SetParent(gameObject.transform, false);
                grid.SetValue(x, y, level[x, y]);
            }
        }
    }
    void Start()
    {
        wires = Levels.Colors.ToDictionary(k => k.name, v =>new Stack<GameObject>());
        grid = new Grid(width, height, cellSize, position);
        grid.VisualizeGrid();
        SetUpLevel(Levels.level1);
    }
    
    void Update()
    {
        var position = GetWorldPosition();
        var (x, y) = grid.GetXY(position);
        if (Input.GetMouseButton(0))
        {
            Debug.Log($"X: {x} Y: {y}");
            if (color != null && grid.IsEmpty(x, y) && grid.AreThereAnyNeighbors(x, y, color.name))
            {
                var clone = GetClone(x, y, color);
                grid.SetValue(position, clone);
                wires[color.name].Push(clone);
            }
            color = grid.GetValue(position);
        }
        if (Input.GetMouseButton(1))
        {
            color = grid.GetValue(position);
            if (wires[color.name].Peek() != grid.GetValue(x, y)) return;
            grid.Delete(x, y);
            wires[color.name].Pop();
        }
    }

    private GameObject GetClone(int x, int y, GameObject obj)
    {
        var clone = Instantiate(
            obj,
            grid.GetWorldPosition(x, y) + new Vector3(cellSize / 2, cellSize / 2),
            Quaternion.identity);
        clone.name = obj.name;
        return clone;
    }

    private Vector3 GetWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}