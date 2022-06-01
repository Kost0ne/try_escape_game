using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrimManager : MonoBehaviour
{
    private bool state = false;
    private bool isFirst = true;
    [SerializeField] private int skrimDelay;
    [SerializeField] private string pumpingSound = "PumpingSound";
    [SerializeField] private string skrimSound = "SkrimSound";
    
    [SerializeField] private GameObject skrim;
    [SerializeField] private GameObject light;
    
    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No audiomanager");
    }
    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (!isFirst) yield break;
        state = true;
        var flashes = light.GetComponents<Flash>();
        flashes[0].enabled = false;
        flashes[1].enabled = true;
        // yield return new WaitForSeconds(3f);
        audioManager.PlaySound(pumpingSound);
        yield return new WaitForSeconds(flashes[1].RandomTimerMAX);
        audioManager.StopSound(pumpingSound);
        audioManager.PlaySound(skrimSound);
        skrim.SetActive(true);
        yield return new WaitForSeconds(skrimDelay);
        flashes[0].enabled = true;
        flashes[1].enabled = false;
        audioManager.StopSound(skrimSound);
        skrim.SetActive(false);
        state = false;
    }

    private void Update()
    {
        if (state)
        {
            isFirst = false;
        }
    }
}
