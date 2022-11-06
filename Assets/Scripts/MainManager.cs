using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private AudioSource mainManagerAudioSource;


    //wave\\
    public int wave;
    //gems\\
    private int totalGems;
    //player\\
    private float Speed { get; set; }
    private float Mass { get; set; }
    private float PowerupForce { get; set; }

    public float Strength { get; set; }
   

    //game\\
    public int difficulty;
    

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        //singleton\\
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        totalGems = LoadGems();
        //mainManagerAudioSource = 

        Instance.mainManagerAudioSource = GetComponent<AudioSource>();

        //TESTING\\
        //Instance.difficulty = 2;


        //if (!Instance.mainManagerAudioSource.isPlaying)
        //{
        //    Instance.mainManagerAudioSource.loop = true;
        //    Instance.mainManagerAudioSource.Play();
        //}
        
        
        

        DontDestroyOnLoad(gameObject);

        
    }

    

   


    //WAVE DATA\\
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

        return 0;
    }


    //GEM DATA\\
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

    public void setTotalGems(int amount)
    {
        totalGems += amount;
    }

    public int getTotalGems()
    {
        return totalGems;
    }

    //PLAYER DATA\\
    class PlayerData
    {
        public float speed;
        public float mass;
        public float powerupForce;
        public float strength;

        public PlayerData(float speed, float mass, float powerupForce, float strength)
        {
            this.speed = speed;
            this.mass = mass;
            this.powerupForce = powerupForce;
            this.strength = strength;
        }
    }

    public void SavePlayer(float speed, float mass, float powerupForce, float strength)
    {
        PlayerData data = new PlayerData(speed, mass, powerupForce, strength);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);

    }

    public void LoadPLayer()
    {
        string path = Application.persistentDataPath + "/player.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            this.Speed = data.speed;
            this.PowerupForce = data.powerupForce;
            this.Strength = data.strength;
            this.Mass = data.mass;
            //return data;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
