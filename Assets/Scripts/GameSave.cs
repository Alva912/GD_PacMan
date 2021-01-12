using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSave : MonoBehaviour
{
    private string path;

    private void Awake()
    {
        path = Application.dataPath + "/GameSave.json";
    }

    public void SaveIntoJson(SaveData _SaveData)
    {
        string json = JsonUtility.ToJson(_SaveData, true);
        File.WriteAllText(path, json);
    }

    public SaveData LoadFromJson()
    {
        string json = File.ReadAllText(path);
        SaveData _LoadData = JsonUtility.FromJson<SaveData>(json);
        return _LoadData;
    }
}
[System.Serializable]
public class SaveData
{
    public PacmanData[] pacmanData;
}

[System.Serializable]
public class PacmanData
{
    public int score;
    public float inGameTime;
}