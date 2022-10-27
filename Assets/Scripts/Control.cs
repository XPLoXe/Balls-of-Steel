using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    public TextMeshProUGUI waveCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWaveCount(SpawnManager.waveCount);
    }

    public void ResetTheGame()
    {
        SpawnManager.waveCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateWaveCount(int wave)
    {
        waveCount.text = "Wave: " + wave.ToString();
    }
}
