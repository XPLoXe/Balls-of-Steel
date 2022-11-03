using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public TextMeshProUGUI highScore;
    public TextMeshProUGUI gemCount;

    //AUDIO\\
    public AudioClip menuMusicClip;
    public AudioClip hardDifficultyClip;
    // Start is called before the first frame update
    void Awake()
    {
        highScore.text = "Highest Score: " + MainManager.Instance.LoadWave().ToString();
        gemCount.text = MainManager.Instance.getTotalGems().ToString();

    }

    private void Start()
    {
        MusicManager.Instance.PlayMusic(menuMusicClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void StarNewEasy()
    {
        //Enemy.difficulty = 1;
        MainManager.Instance.difficulty = 1;
        StartNew();
    }

    public void StartNewHard()
    {
        //Enemy.difficulty = 2;
        MusicManager.Instance.PlayEffect(hardDifficultyClip);
        MainManager.Instance.difficulty = 2;

        StartNew();
    }
}
