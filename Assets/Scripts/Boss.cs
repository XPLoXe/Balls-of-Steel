using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        setSpeed();
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
        WaterBounds();
        LowerBounds();
    }
}
