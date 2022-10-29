using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9.0f;
    public static int waveCount = 0;
    private AudioSource managerAudioSource;
    public AudioClip waveClip;
    public PlayerController playerControllerScript;

    //enemy\\
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float enemyStartDelay = 1.0f;
    public float enemySpawnInterval = 3.0f;
    public int enemyCount;

    //powerup\\
    public GameObject powerupPrefab;
    public float powerupStartDelay = 5.0f;
    public float powerupSpawnInterval = 20.0f;

    //gem\\
    public GameObject gemPrefab;

    //UI
    public TextMeshProUGUI waveText;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        managerAudioSource = GetComponent<AudioSource>();
        //InvokeRepeating("SpawnEnemy", enemyStartDelay, enemySpawnInterval);
        InvokeRepeating("SpawnPowerUp", powerupStartDelay, powerupSpawnInterval);
        InvokeRepeating("SpawnGem", powerupStartDelay, powerupSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0 && playerControllerScript.gameOver == false)
        {
            managerAudioSource.PlayOneShot(waveClip);
            
            waveCount++;
            MainManager.Instance.wave = waveCount;
            Debug.Log(MainManager.Instance.wave);
            UpdateWaveCount(waveCount);
            spawnEnemyWave(waveCount * 2);
            SpawnPowerUp();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosz = Random.Range(-spawnRange, spawnRange);
        float spawnPosx = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosx, 0, spawnPosz);
        return randomPos;
    }

    private void spawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }

        if (waveCount%4 == 0)
        {
            SpawnBoss();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    }

    private void SpawnPowerUp()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + new Vector3(0, 1, 0), powerupPrefab.transform.rotation);
        }
        
    }

    //GEM\\
    private void SpawnGem()
    {
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(gemPrefab, GenerateSpawnPosition() + new Vector3(0, 0.05f, 0), gemPrefab.transform.rotation);
        }
    }

    public void UpdateWaveCount(int wave)
    {
        waveText.text = "Wave: " + wave.ToString();
    }
}
