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
   GameMaster gameMaster;

   private void Start()
   {
      audioManager = AudioManager.instance;
      if (audioManager == null)
         Debug.LogError("No audiomanager");
      
      gameMaster = GameMaster.instance;
      if (gameMaster == null)
         Debug.LogError("No GameMaster");
   }

   public void StartGame()
   {
      GameMaster.IsLoad = true;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Continue()
   {
      GameMaster.IsLoad = false;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      gameMaster.LoadGame();
      // asyncLoad.
      // while (!asyncLoad.isDone) continue;
      // LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   
   // private IEnumerator LoadYourAsyncScene(int sceneIndex) 
   // {
   //    var asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
   //
   //    while (!asyncLoad.isDone)
   //       yield return null;
   //    
   //    gameMaster.LoadGame();
   //    Debug.Log("Load");
   //    yield return null;
   // }


   // private IEnumerator LoadGame()
   // {
   //    yield return new WaitForEndOfFrame();
   //    gameMaster.LoadGame();
   // }
   
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
