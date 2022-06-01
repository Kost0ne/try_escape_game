using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
        public static void SaveGame(GameMaster gameMaster)
        {
                var formatter = new BinaryFormatter();
                var path = Application.persistentDataPath + "/save.fun";
                var stream = new FileStream(path, FileMode.Create);

                var data = new GameData(gameMaster);
                
                formatter.Serialize(stream, data);
                stream.Close();
                Debug.Log("Saved: " + path);
        }

        public static GameData LoadGame()
        {
                var path = Application.persistentDataPath + "/save.fun";
                if (File.Exists(path))
                {
                     var formatter = new BinaryFormatter();
                     var stream = new FileStream(path, FileMode.Open);

                     var data = formatter.Deserialize(stream) as GameData;
                     stream.Close();
                     
                     return data;
                }
                Debug.Log("Save file not found in" + path);
                return null;
        }
}