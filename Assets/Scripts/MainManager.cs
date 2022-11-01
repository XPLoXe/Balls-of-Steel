using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private AudioSource mainManagerAudioSource;

    public int wave;
    private int totalGems;

    public int difficulty;
    

    // Start is called before the first frame update
    void Awake()
    {
        //singleton\\
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        totalGems = LoadGems();
        mainManagerAudioSource = GetComponent<AudioSource>();

        Instance.mainManagerAudioSource = mainManagerAudioSource;



        if (!Instance.mainManagerAudioSource.isPlaying)
        {
            Instance.mainManagerAudioSource.loop = true;
            Instance.mainManagerAudioSource.Play();
        }
        
        
        

        DontDestroyOnLoad(gameObject);

        
    }

    public void setTotalGems(int amount)
    {
        totalGems += amount;
    }

    public int getTotalGems()
    {
        return totalGems;
    }

    class SaveData
    {
        public int wave;
        public SaveData(int waveToSave)
        {
            wave = waveToSave;
        }
        
    }

    class GemData
    {
        public int gems;

        public GemData(int gems)
        {
            this.gems = gems;
        }
    }

    public void SaveGems()
    {
        GemData data = new GemData(totalGems);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/gems.json", json);
    }

    public int LoadGems()
    {
        string path = Application.persistentDataPath + "/gems.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GemData data = JsonUtility.FromJson<GemData>(json);
            setTotalGems(data.gems);
            return data.gems;
        }

        return 0;
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

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
