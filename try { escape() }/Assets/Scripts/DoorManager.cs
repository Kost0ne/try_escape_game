using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private string doorSound = "DoorSound";

    AudioManager audioManager;
    void Start()
    {
        animator = doorObject.GetComponent<Animator>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No audiomanager");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpen) return;
        Debug.Log("Animation");
        audioManager.PlaySound(doorSound);
        animator.SetTrigger("isTriggered");
        isOpen = true;
    }
}
