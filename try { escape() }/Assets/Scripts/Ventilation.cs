using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ventilation : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField]
    private string ventilationSound = "VentilationSound";
    [SerializeField]
    private GameObject openVentilationObj;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No audiomanager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioManager.PlaySound(ventilationSound);
        openVentilationObj.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        audioManager.PlaySound(ventilationSound);
        openVentilationObj.SetActive(false);
    }
}
