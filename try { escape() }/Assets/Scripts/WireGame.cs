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
                if (!wires.ContainsKey(level[x, y].name))
                    wires.Add(level[x, y].name, new Wire((x, y), level[x, y]));
                else
                {
                    wires[level[x, y].name].AddFinish(x, y);
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
        if (IsWin()) Debug.Log("Victory!");
        var position = GetWorldPosition();
        var (x, y) = grid.GetXY(position);
        if (Input.GetMouseButton(0))
        {
            if (color != null && (x, y) == wires[color.name].Finish)
                wires[color.name].Add(x, y);
            if (color != null && wires[color.name].CanAdd(x, y) && grid.IsEmpty(x, y))
            {
                var clone = GetClone(x, y, color);
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