using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public bool HasMount;
    public bool HasCard;
    public bool HasScrewdriver;
    public bool HasFlashlight;
    public bool IsFireQuestCompleted;
    public bool IsWireQuestCompleted;
    public bool IsMouseQuestCompleted;
    public bool IsCardQuestCompleted;
    public Vector3 position;

    public static bool IsLoad = true;
    
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
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
        IsLoad = false;
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
        position = new Vector3(data.position[0], data.position[1], data.position[2]);
        print(position);
    }

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) return;
        if (!IsLoad)
        {
            IsLoad = true;
            player.transform.position = position;
        }
        position = player.transform.position;
    }
    
}
