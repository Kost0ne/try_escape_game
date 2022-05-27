using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField]
   private string hoverOverSound = "ButtonHover";
   
   [SerializeField]
   private string pressButtonSound = "ButtonPress";
   
   AudioManager audioManager;

   private void Start()
   {
      audioManager = AudioManager.instance;
      if (audioManager == null)
         Debug.LogError("No audiomanager");
   }

   public void StartGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void ExitGame()
   {
      Debug.Log("Exit");
      Application.Quit();
   }

   public void OnMouseOver()
   {
      audioManager.PlaySound(hoverOverSound);
   }

   public void OnMouseDown()
   {
      audioManager.PlaySound(pressButtonSound);
   }
}
