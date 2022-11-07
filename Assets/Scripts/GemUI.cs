using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class GemUI : MonoBehaviour
{


    private float speed;
    private float mass;
    private float powerupForce;
    private float strength;
    private int cantSpeed;
    private int cantPowerupForce;
    private int cantStrength;
    private int cantMass;
     

    void Awake()
    {
        cantMass = PlayerDataManager.Instance.cantMass;
        cantSpeed = PlayerDataManager.Instance.cantSpeed;
        cantPowerupForce = PlayerDataManager.Instance.cantPowerupForce;
        cantStrength = PlayerDataManager.Instance.cantStrength;
        speed = PlayerDataManager.Instance.Speed;
        mass = PlayerDataManager.Instance.Mass;
        powerupForce = PlayerDataManager.Instance.PowerupForce;
        strength = PlayerDataManager.Instance.Strength;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
