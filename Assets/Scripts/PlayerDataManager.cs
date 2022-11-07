using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }


    //player\\
    public float Speed { get; set; }
    public float Mass { get; set; }
    public float PowerupForce { get; set; }
    public float Strength { get; set; }
    public int cantSpeed { get; set; }
    public int cantPowerupForce { get; set; }
    public int cantStrength { get; set; }
    public int cantMass { get; set; }

    void Awake()
    {
        //singleton\\
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadPLayer();

        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //PLAYER DATA\\
    class PlayerData
    {
        //attributes
        public float speed;
        public float mass;
        public float powerupForce;
        public float strength;
        //counters for how many upgrades the player had
        public int cantSpeed;
        public int cantPowerupForce;
        public int cantStrength;
        public int cantMass;

        public PlayerData(float speed, float mass, float powerupForce, float strength, int cantSpeed, int cantPowerupForce, int cantStrength, int cantMass)
        {
            this.speed = speed;
            this.mass = mass;
            this.powerupForce = powerupForce;
            this.strength = strength;
            this.cantSpeed = cantSpeed;
            this.cantPowerupForce = cantPowerupForce;
            this.cantStrength = cantStrength;
            this.cantMass = cantMass;
        }
    }

    public void SavePlayer(float speed, float mass, float powerupForce, float strength, int cantSpeed, int cantPowerupForce, int cantStrength, int cantMass)
    {
        PlayerData data = new PlayerData(speed, mass, powerupForce, strength, cantSpeed, cantPowerupForce, cantStrength, cantMass);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);

    }

    public void SavePlayer()
    {
        PlayerData data = new PlayerData(this.Speed, this.Mass, this.PowerupForce, this.Strength, this.cantSpeed, this.cantPowerupForce, this.cantStrength, this.cantMass);
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
            this.cantSpeed = data.cantSpeed;
            this.cantPowerupForce = data.cantPowerupForce;
            this.cantStrength = data.cantStrength;
            this.cantMass = data.cantMass;
            //return data;
        }
        else
        {
            PlayerData data = new PlayerData(5f, 1f, 15f, 3f, 1, 1, 1, 1); //default values for new players
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/player.json", json);
            this.Speed = 5f;
            this.PowerupForce = 15f;
            this.Strength = 3f;
            this.Mass = 1;
            this.cantSpeed = 1;
            this.cantPowerupForce = 1;
            this.cantStrength = 1;
            this.cantMass = 1;
        }
    }

    public void addSpeed()
    {
        this.Speed *= 1.10f;
        this.cantSpeed++;
        SavePlayer();
    }

    public void addPowerUpForce()
    {
        this.PowerupForce *= 1.10f;
        this.cantPowerupForce++;
        SavePlayer();
    }

    public void addStrength()
    {
        this.Strength *= 1.10f;
        this.cantStrength++;
        SavePlayer();
    }
    public void addMass()
    {
        this.Mass *= 1.10f;
        this.cantMass++;
        SavePlayer();
    }



}
