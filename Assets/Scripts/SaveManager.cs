using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string level_id;
    public SaveData saveData;

    public static SaveManager instance { get; private set; }
    // Start is called before the first frame update

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        Load();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            saveData = data;
            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.dat");

        bf.Serialize(file, saveData);
        file.Close();
    }

    public void CollectBattery(string batteryId)
    {
        saveData.batteries.Add(batteryId);
        Save();
    }

    public void Death()
    {
        switch (level_id)
        {
            case "Level 1":
                saveData.level1_deaths += 1;
                break;
            case "Level 2":
                saveData.level2_deaths += 1;
                break;
            case "Level 3":
                saveData.level3_deaths += 1;
                break;
        }
        Save();
    }

    public void Finish()
    {
        switch (level_id)
        {
            case "Level 1":
                saveData.level1_completed = true;
                break;
            case "Level 2":
                saveData.level2_completed = true;
                break;
            case "Level 3":
                saveData.level3_completed = true;
                break;
        }
        Save();
    }

    public void SetMusicVolume(float value)
    {
        saveData.music = value;
        Save();
    }

    public void SetSoundVolume(float value)
    {
        saveData.sound = value;
        Save();
    }

}
[Serializable]
public class SaveData
{
    public List<string> batteries;
    public bool level1_completed;
    public int level1_deaths;
    public bool level2_completed;
    public int level2_deaths;
    public bool level3_completed;
    public int level3_deaths;
    public float sound = 1f;
    public float music = 1f;
}


