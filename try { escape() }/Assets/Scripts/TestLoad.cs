using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = GameObject.FindGameObjectsWithTag("Player")[0];
        Debug.Log(player);
    }
}
