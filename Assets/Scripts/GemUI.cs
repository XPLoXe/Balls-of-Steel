using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.UIElements;

public class GemUI : MonoBehaviour
{
    private const int baseCost = 5;

    //UI\\
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI powerupText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI massText;

    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI powerupCostText;
    public TextMeshProUGUI strengthCostText;
    public TextMeshProUGUI massCostText;

    public TextMeshProUGUI gemCount;

    public GameObject addSpeed;
    public GameObject addPowerup;
    public GameObject addStrength;
    public GameObject addMass;

    public GameObject lockSpeed;
    public GameObject lockPowerup;
    public GameObject lockStrength;
    public GameObject lockMass;

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
        speedText.text = "Speed: " + speed.ToString("F2");
        powerupText.text = "Power Up: " + powerupForce.ToString("F2");
        strengthText.text = "Strength: " + strength.ToString("F2");
        massText.text = "Mass: " + mass.ToString("F2");

        speedCostText.text = (baseCost * cantSpeed).ToString();
        powerupCostText.text = (baseCost * cantPowerupForce).ToString();
        strengthCostText.text = (baseCost * cantStrength).ToString();
        massCostText.text = (baseCost * cantMass).ToString();

        if (MainManager.Instance.getTotalGems() < (baseCost * cantSpeed))
        {
            addSpeed.SetActive(false);
            lockSpeed.SetActive(true);
        }
        
        if (MainManager.Instance.getTotalGems() < (baseCost * cantPowerupForce))
        {
            addPowerup.SetActive(false);
            lockPowerup.SetActive(true);
        }
        
        if (MainManager.Instance.getTotalGems() < (baseCost * cantStrength))
        {
            addStrength.SetActive(false);
            lockStrength.SetActive(true);
        }
        
        if (MainManager.Instance.getTotalGems() < (baseCost * cantMass))
        {
            addMass.SetActive(false);
            lockMass.SetActive(true);
        }

        gemCount.text = MainManager.Instance.getTotalGems().ToString();
    }

    public void AddSpeed()
    {
        MainManager.Instance.updateGems(-baseCost * cantSpeed);
        PlayerDataManager.Instance.addSpeed();
        cantSpeed = PlayerDataManager.Instance.cantSpeed;
        speed = PlayerDataManager.Instance.Speed;
    }

    public void AddPowerUpForce()
    {
        MainManager.Instance.updateGems(-baseCost * cantPowerupForce);
        PlayerDataManager.Instance.addPowerUpForce();
        cantPowerupForce = PlayerDataManager.Instance.cantPowerupForce;
        powerupForce = PlayerDataManager.Instance.PowerupForce;
    }

    public void AddStrength()
    {
        MainManager.Instance.updateGems(-baseCost * cantStrength);
        PlayerDataManager.Instance.addStrength();
        cantStrength = PlayerDataManager.Instance.cantStrength;
        strength = PlayerDataManager.Instance.Strength;
    }
    public void AddMass()
    {
        MainManager.Instance.updateGems(-baseCost * cantMass);
        PlayerDataManager.Instance.addMass();
        cantMass = PlayerDataManager.Instance.cantMass;
        mass = PlayerDataManager.Instance.Mass;
    }


}
