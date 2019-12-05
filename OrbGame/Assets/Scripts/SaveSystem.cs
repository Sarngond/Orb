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

    public static void SaveButtons(GameObject button) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + button.name + ".orb";
        FileStream stream = new FileStream(path, FileMode.Create);

        ButtonData data = new ButtonData(button);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ButtonData LoadButtons(GameObject button) {
        string path = Application.persistentDataPath + "/" + button.name + ".orb";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ButtonData data = formatter.Deserialize(stream) as ButtonData;
            stream.Close();

            Debug.Log("Loaded Buttons");

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveKeyCard(GameObject card) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + card.name + ".orb";
        FileStream stream = new FileStream(path, FileMode.Create);

        KeyCardData data = new KeyCardData(card);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static KeyCardData LoadKeyCard(GameObject card) {
        string path = Application.persistentDataPath + "/" + card.name + ".orb";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            KeyCardData data = formatter.Deserialize(stream) as KeyCardData;
            stream.Close();

            Debug.Log("Loaded KeyCard");

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveGenerator(GameObject generator) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + generator.name + ".orb";
        FileStream stream = new FileStream(path, FileMode.Create);

        GeneratorData data = new GeneratorData(generator);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GeneratorData LoadGenerator(GameObject generator) {
        string path = Application.persistentDataPath + "/" + generator.name + ".orb";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GeneratorData data = formatter.Deserialize(stream) as GeneratorData;
            stream.Close();

            Debug.Log("Loaded Generator");

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
