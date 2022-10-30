using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bomber : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        setSpeed();
        setWallForceMultiplier(5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
