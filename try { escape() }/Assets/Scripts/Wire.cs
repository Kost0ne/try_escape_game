using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public (int, int) position;
    private GameObject prefab;

    public Node((int, int) position, GameObject prefab)
    {
        this.position = position;
        this.prefab = prefab;
    }
}

public class Wire
{
    private (int, int) last;
    private (int, int) start;
    private (int, int) finish;
    private Stack<Node> stack;
    private GameObject prefab;
    
    public (int, int) Finish => finish;
    
    public Wire((int, int) start, GameObject prefab)
    {
        last = start;
        this.prefab = prefab;
        this.start = start;
        stack = new Stack<Node>();
    }
    public void Add(int x, int y)
    {
        if ((x, y) == finish) return;
        last = (x, y);
        stack.Push(new Node(last, prefab));
    }

    public void Remove()
    {
        stack.Pop();
        last = stack.Count != 0
            ? stack.Peek().position
            : start;
    }

    public bool CanAdd(int x, int y)
    {
        for (var dy = -1; dy <= 1; dy++)
        for (var dx = -1; dx <= 1; dx++)
        {
            if (dx != 0 && dy != 0) continue;
            if (last == (x + dx, y + dy)) return true;
        }
        return false;
    }
    
    public bool CanRemove(int x, int y)
    {
        return (x, y) == last;
    }

    public void AddFinish(int x, int y)
    {
        finish = (x, y);
    }
    
    public bool IsConnected() => last == finish;

}