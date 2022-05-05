using System.Collections.Generic;
using UnityEngine;

public class ColorNode : MonoBehaviour
{
    public string Name;
    public GameObject end;
    public GameObject start;
    public GameObject straight;
    public GameObject twist;

    public ColorNode(string name, GameObject end, GameObject start, GameObject straight, GameObject twist)
    {
        Name = name;
        this.end = end;
        this.start = start;
        this.straight = straight;
        this.twist = twist;
    }
}

public class Levels : MonoBehaviour
{
    [SerializeField] private List<string> colors;
    // [SerializeField] private GameObject green;
    // [SerializeField] private GameObject blue;
    // [SerializeField] private GameObject yellow;
    // [SerializeField] private GameObject purple;
    // [SerializeField] private GameObject azure;

    public static ColorNode[,] level1;

    private ColorNode GetColorNode(string name)
    {
        var straight = Resources.Load<GameObject>($"WiresPrefabs/{name}");
        var end = Resources.Load<GameObject>($"WiresPrefabs/{name}_end");
        var start = Resources.Load<GameObject>($"WiresPrefabs/{name}_start");
        var twist = Resources.Load<GameObject>($"WiresPrefabs/{name}_twist");
        
        return new ColorNode(name, end, start, straight, twist);
    }
    void Awake()
    {
        var colorNodes = new List<ColorNode>();
        foreach (var color in colors)
            colorNodes.Add(GetColorNode(color));

        level1 = new[,]{
            {colorNodes[0], null, null, null, colorNodes[1]},
            {null, null, colorNodes[2], null, null},
            {colorNodes[0], null, null, colorNodes[1], null},
            {colorNodes[3], null, null, colorNodes[3], colorNodes[2]},
            {colorNodes[4], null, null, null, colorNodes[4]}
        };
    }
}