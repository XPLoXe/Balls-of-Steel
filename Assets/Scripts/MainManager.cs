using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int wave;

    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        
    }

    class SaveData
    {
        public int wave;
        public SaveData(int waveToSave)
        {
            wave = waveToSave;
        }
        
    }

    public void SaveWave(int waveToSave)
    {
        SaveData data = new SaveData(waveToSave);
        //data.wave = wave;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highWave.json", json);
        
    }

    public int LoadWave()
    {
        string path = Application.persistentDataPath + "/highWave.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //wave = data.wave;

            return data.wave;
        }

        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
