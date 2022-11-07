using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    private AudioSource mainManagerAudioSource;
    //private PlayerData playerData;

    //wave\\
    public int wave;
    //gems\\
    private int totalGems;



    //player\\
    //public float Speed { get; set; }
    //public float Mass { get; set; }
    //public float PowerupForce { get; set; }
    //public float Strength { get; set; }
    //public int cantSpeed { get; set; }
    //public int cantPowerupForce { get; set; }
    //public int cantStrength { get; set; }
    //public int cantMass { get; set; }


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

        //LoadPLayer();
        

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

    public void updateGems(int amount)
    {
        setTotalGems(amount);
        SaveGems();
    }

    ////PLAYER DATA\\
    //class PlayerData
    //{
    //    //attributes
    //    public float speed;
    //    public float mass;
    //    public float powerupForce;
    //    public float strength;
    //    //counters for how many upgrades the player had
    //    public int cantSpeed;
    //    public int cantPowerupForce;
    //    public int cantStrength;
    //    public int cantMass;

    //    public PlayerData(float speed, float mass, float powerupForce, float strength, int cantSpeed, int cantPowerupForce, int cantStrength, int cantMass)
    //    {
    //        this.speed = speed;
    //        this.mass = mass;
    //        this.powerupForce = powerupForce;
    //        this.strength = strength;
    //        this.cantSpeed = cantSpeed;
    //        this.cantPowerupForce = cantPowerupForce;
    //        this.cantStrength = cantStrength;
    //        this.cantMass = cantMass;
    //    }
    //}

    //public void SavePlayer(float speed, float mass, float powerupForce, float strength, int cantSpeed, int cantPowerupForce, int cantStrength, int cantMass)
    //{
    //    PlayerData data = new PlayerData(speed, mass, powerupForce, strength, cantSpeed, cantPowerupForce, cantStrength, cantMass);
    //    string json = JsonUtility.ToJson(data);
    //    File.WriteAllText(Application.persistentDataPath + "/player.json", json);

    //}

    //public void LoadPLayer()
    //{
    //    string path = Application.persistentDataPath + "/player.json";

    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
    //        this.Speed = data.speed;
    //        this.PowerupForce = data.powerupForce;
    //        this.Strength = data.strength;
    //        this.Mass = data.mass;
    //        this.cantSpeed = data.cantSpeed;
    //        this.cantPowerupForce = data.cantPowerupForce;
    //        this.cantStrength = data.cantStrength;
    //        this.cantMass = data.cantMass;
    //        //return data;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
