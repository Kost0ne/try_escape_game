using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject yellow;
    [SerializeField] private GameObject purple;
    [SerializeField] private GameObject azure;

    public static GameObject[,] level1;
    public static List<GameObject> Colors;

    void Awake()
    {
        Colors = new List<GameObject> {green, blue, yellow, purple, azure};
        level1 = new[,]{
            {green, null, null, null, purple},
            {null, null, yellow, null, null},
            {green, null, null, purple, null},
            {blue, null, null, blue, yellow},
            {azure, null, null, null, azure}
        };
    }
}