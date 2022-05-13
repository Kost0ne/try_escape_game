using System;
using System.Collections.Generic;
using UnityEngine;

public class WireGame : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 position;
    private Dictionary<string, Wire> wires;
    private GameObject color;
    private Grid grid;

    private void SetUpLevel(ColorNode[,] level)
    {
        for(var y = 0; y < width; y++)
        {
            for (var x = 0; x < height; x++)
            {
                try
                {
                    if (level[x, y].Name == null) continue;
                }
                catch (Exception e)
                {
                    continue;
                }
                if (!wires.ContainsKey(level[x, y].Name))
                {
                    wires.Add(level[x, y].Name, new Wire((x, y), level[x, y]));
                    grid.SetValue(x, y, level[x, y].straight);
                    var clone = GetClone(x, y, level[x, y].Name, level[x, y].start);
                    clone.transform.SetParent(gameObject.transform, false);
                }
                else
                {
                    wires[level[x, y].Name].AddFinish(x, y);
                    grid.SetValue(x, y, level[x, y].straight);
                    var clone = GetClone(x, y, level[x, y].Name, level[x, y].end);
                    clone.transform.SetParent(gameObject.transform, false);
                }
            }
        }
    }
    void Start()
    {
        wires = new Dictionary<string, Wire>();
        grid = new Grid(width, height, cellSize, position);
        grid.VisualizeGrid();
        SetUpLevel(Levels.level1);
    }
    
    void Update()
    {
        // if (IsWin()) Debug.Log("Victory!");
        var position = GetWorldPosition();
        var (x, y) = grid.GetXY(position);
        if (Input.GetMouseButton(0))
        {
            if (color != null && (x, y) == wires[color.name].Finish)
                wires[color.name].Add(x, y);
            if (color != null && wires[color.name].CanAdd(x, y) && grid.IsEmpty(x, y))
            {
                var clone = GetClone(x, y, color.name, color);
                grid.SetValue(position, clone);
                wires[color.name].Add(x, y);
            }
            color = grid.GetValue(position);
        }
        if (Input.GetMouseButton(1))
        {
            color = grid.GetValue(position);
            if (!wires[color.name].CanRemove(x, y)) return;
            grid.Delete(x, y);
            wires[color.name].Remove();
        }
    }

    private GameObject GetClone(int x, int y, string objName, GameObject obj)
    {
        var clone = Instantiate(
            obj,
            grid.GetWorldPosition(x, y) + new Vector3(cellSize / 2, cellSize / 2),
            Quaternion.identity);
        clone.name = objName;
        return clone;
    }

    private Vector3 GetWorldPosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool IsWin()
    {
        foreach (var wire in wires.Values)
        {
            if (!wire.IsConnected())
                return false;
        }
        return true;
    }
}