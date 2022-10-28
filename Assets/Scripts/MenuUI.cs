using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public TextMeshProUGUI highScore;
    // Start is called before the first frame update
    void Awake()
    {
        highScore.text = "Highest Score: " + MainManager.Instance.LoadWave().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(0);
    }

    public void StarNewEasy()
    {
        Enemy.difficulty = 1;
        StartNew();
    }

    public void StartNewHard()
    {
        Enemy.difficulty = 2;
        StartNew();
    }
}
