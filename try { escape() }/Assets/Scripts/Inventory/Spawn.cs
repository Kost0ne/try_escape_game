using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        var offset = UnityEngine.Random.Range(0.5f, 1f);
        var playerPos = new Vector2(player.position.x + offset, player.position.y - player.transform.localScale.y - 0.1f);
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
