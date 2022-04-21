using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject orange;
    [SerializeField] private GameObject purple;
    [SerializeField] private GameObject red;

    public static GameObject[,] level1;
    public static List<GameObject> Colors;

    void Start()
    {
        Colors = new List<GameObject> {green, blue, orange, purple, red};
        level1 = new[,]{
            {green, null, null, null, purple},
            {null, null, orange, null, null},
            {green, null, null, purple, null},
            {blue, null, null, blue, orange},
            {red, null, null, null, red}
        };
    }
}