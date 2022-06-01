using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    GameMaster gameMaster;
    
    private void Start()
    {
        gameMaster = GameMaster.instance;
        if (gameMaster == null)
            Debug.LogError("No GameMaster");
    }
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (GameIsPaused) Resume();
        else Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseMenuUI.SetActive(true);
    }

    public void GoToMenu()
    {
        Resume();
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {
        gameMaster.SaveGame();
        Debug.Log("Exit");
        Application.Quit();
    }
}
