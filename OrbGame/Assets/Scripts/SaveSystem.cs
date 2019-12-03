using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer (GameObject player) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.orb";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer () {
        string path = Application.persistentDataPath + "/player.orb";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveGuard(GameObject guard) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + guard.name +".orb";
        FileStream stream = new FileStream(path, FileMode.Create);

        GuardData data = new GuardData(guard);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GuardData LoadGuard(GameObject guard) {
        string path = Application.persistentDataPath + "/" + guard.name + ".orb";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GuardData data = formatter.Deserialize(stream) as GuardData;
            stream.Close();

            Debug.Log("Loaded Guards");

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
