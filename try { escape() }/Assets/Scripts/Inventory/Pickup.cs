using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         for (var i = 0; i < inventory.slots.Length; i++)
    //         {
    //             if (inventory.isFull[i] == false)
    //             {
    //                 inventory.isFull[i] = true;
    //                 Instantiate(slotButton, inventory.slots[i].transform);
    //                 Destroy(gameObject);
    //                 break;
    //             }
    //         }
    //     }
    // }

    private void OnMouseDown()
    {
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
        for (var i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i]) continue;
            inventory.isFull[i] = true;
            Instantiate(slotButton, inventory.slots[i].transform);
            Destroy(gameObject);
            break;
        }
    }
}
