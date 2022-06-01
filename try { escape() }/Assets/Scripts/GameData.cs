using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool HasMount;
    public bool HasCard;
    public bool HasFlashlight;
    public bool HasScrewdriver;
    
    public bool IsFireQuestCompleted;
    public bool IsWireQuestCompleted;
    public bool IsMouseQuestCompleted;
    public bool IsCardQuestCompleted;

    public float[] position;

    public GameData(GameMaster gameMaster)
    {
        HasMount = gameMaster.HasMount;
        HasCard = gameMaster.HasCard;
        HasFlashlight = gameMaster.HasFlashlight;
        HasScrewdriver = gameMaster.HasScrewdriver;
        
        IsFireQuestCompleted = gameMaster.IsFireQuestCompleted;
        IsWireQuestCompleted = gameMaster.IsWireQuestCompleted;
        IsMouseQuestCompleted = gameMaster.IsMouseQuestCompleted;
        IsCardQuestCompleted = gameMaster.IsCardQuestCompleted;

        position = new float[3];
        position[0] = gameMaster.transform.position.x;
        position[1] = gameMaster.transform.position.y;
        position[2] = gameMaster.transform.position.z;
    }

    // private void SetPlayerPosition()
    // {
    //     var player = GameObject.FindGameObjectsWithTag("Player")[0];
    //     position = new float[3];
    //     position[0] = player.transform.position.x;
    //     position[1] = player.transform.position.y;
    //     position[2] = player.transform.position.z;
    // }
}