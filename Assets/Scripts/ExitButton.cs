using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }

    public void BackToMenu()
    {
        MusicManager.Instance.musicSource.Pause();
        SpawnManager.waveCount = 0;
        SceneManager.LoadScene(0);
    }
}
