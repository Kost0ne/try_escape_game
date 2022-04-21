using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public (int, int) position;
    public GameObject prefab;

    public Node((int, int) position, GameObject prefab)
    {
        this.position = position;
        this.prefab = prefab;
    }
}

public class Wire
{
    private (int, int) last;
    private Stack<Node> stack;
    private GameObject prefab;

    public Wire((int, int) start, GameObject prefab)
    {
        last = start;
        this.prefab = prefab;
        stack = new Stack<Node>();
    }
    public void Add(int x, int y)
    {
        last = (x, y);
        stack.Push(new Node(last, prefab));
    }

    public void Remove()
    {
        stack.Pop();
        last = stack.Peek().position;
    }

}