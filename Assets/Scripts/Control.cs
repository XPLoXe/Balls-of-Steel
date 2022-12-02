using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTheGame();
        }
    }

    public void ResetTheGame()
    {
        SpawnManager.waveCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
