using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public bool HasMount = false;
    public bool HasCard = false;
    public bool HasScrewdriver = false;
    public bool HasFlashlight = false;
    public bool IsFireQuestCompleted = false;
    public bool IsWireQuestCompleted = false;
    public bool IsMouseQuestCompleted = false;
    public bool IsCardQuestCompleted = false;
    
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        var data = SaveSystem.LoadGame();
        
        HasMount = data.HasMount;
        HasCard = data.HasCard;
        HasScrewdriver = data.HasScrewdriver;
        HasFlashlight = data.HasFlashlight;
        IsFireQuestCompleted = data.IsFireQuestCompleted;
        IsWireQuestCompleted = data.IsWireQuestCompleted;
        IsMouseQuestCompleted = data.IsMouseQuestCompleted;
        IsCardQuestCompleted = data.IsCardQuestCompleted;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        Debug.Log(position);
        gameObject.transform.position = position;
    }
}
