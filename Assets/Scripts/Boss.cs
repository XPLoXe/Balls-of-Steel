using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    //AUDIO\\
    private AudioSource bossAudioSource;
    public AudioClip bossHitClip;


    // Start is called before the first frame update
    void Start()
    {
        bossAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        setSpeed();
        setWallForceMultiplier(1.5f);
    }

    public override void setSpeed()
    {
        if (difficulty == 2)
        {
            speed = 25.0f;
        }

        if (difficulty == 1)
        {
            speed = 30.0f;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision.gameObject))
        {
            bossAudioSource.PlayOneShot(bossHitClip);
        }
    }


}
