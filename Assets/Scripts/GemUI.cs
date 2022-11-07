using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.UIElements;

public class GemUI : MonoBehaviour
{
    //UI\\
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI massText;

    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI powerupCostText;
    public TextMeshProUGUI strengthCostText;
    public TextMeshProUGUI massCostText;


    //player data\\
    private float speed;
    private float powerupForce;
    private float strength;
    private float mass;

    private int cantSpeed;
    private int cantPowerupForce;
    private int cantStrength;
    private int cantMass;
     

    void Awake()
    {
        cantSpeed = PlayerDataManager.Instance.cantSpeed;
        cantPowerupForce = PlayerDataManager.Instance.cantPowerupForce;
        cantStrength = PlayerDataManager.Instance.cantStrength;
        cantMass = PlayerDataManager.Instance.cantMass;

        speed = PlayerDataManager.Instance.Speed;
        powerupForce = PlayerDataManager.Instance.PowerupForce;
        strength = PlayerDataManager.Instance.Strength;
        mass = PlayerDataManager.Instance.Mass;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + speed.ToString();
        powerupText.text = "Power Up: " + powerupForce.ToString();
        strengthText.text = "Strength: " + strength.ToString();
        massText.text = "Mass: " + mass.ToString();

        speedCostText.text = (5 * cantSpeed).ToString();
        powerupCostText.text = (5 * cantPowerupForce).ToString();
        strengthCostText.text = (5 * cantStrength).ToString();
        massCostText.text = (5 * cantMass).ToString();
    }

    public void addSpeed()
    {
        MainManager.Instance.updateGems(-5 * cantSpeed);
        PlayerDataManager.Instance.addSpeed();
        cantSpeed = PlayerDataManager.Instance.cantSpeed;
    }

    public void addPowerUpForce()
    {
        MainManager.Instance.updateGems(-5 * cantPowerupForce);
        PlayerDataManager.Instance.addPowerUpForce();
        cantPowerupForce = PlayerDataManager.Instance.cantPowerupForce;
    }

    public void addStrength()
    {
        MainManager.Instance.updateGems(-5 * cantStrength);
        PlayerDataManager.Instance.addStrength();
        cantStrength = PlayerDataManager.Instance.cantStrength;
    }
    public void addMass()
    {
        MainManager.Instance.updateGems(-5 * cantMass);
        PlayerDataManager.Instance.addMass();
        cantMass = PlayerDataManager.Instance.cantMass;
    }


}
