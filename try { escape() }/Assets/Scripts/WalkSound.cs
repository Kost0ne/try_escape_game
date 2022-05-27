using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioManager audioManager;
    [SerializeField]
    private string stepSound = "StepSound";

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
            Debug.LogError("No audiomanager");
    }

    public void PlayStepSound()
    {
        audioManager.PlaySound(stepSound);
    }
}
