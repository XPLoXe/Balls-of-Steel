using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    //singleton\\
    public static MusicManager Instance;

    //Audio
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioClip mainMenuMusic;
    //public AudioClip mainMenuMusic;
    //public AudioClip lvl1Music;
    //public AudioClip lvl2Music;

    //random pitch adjustment range
    public float LowPitchRange = 0.95f;
    public float HighPitchRange = 1.05f;



    void Awake()
    {
        //singleton\\
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //else if (Instance == null)
        //{
        //    Instance = this;
        //}

        Instance = this;


        //musicAudioSource = GetComponent<AudioSource>();

        //if (Instance.musicSource.isPlaying) return;

        //Instance.PlayMusic(mainMenuMusic);

        Instance.PlayMusic(mainMenuMusic);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        effectSource.pitch = randomPitch;
        effectSource.clip = clips[randomIndex];
        effectSource.Play();
    }


}
