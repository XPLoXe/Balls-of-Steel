using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Enemy
{
    //AUDIO\\
    private AudioSource hunterAudioSource;
    public AudioClip hunterHitClip;

    // Start is called before the first frame update
    void Start()
    {
        hunterAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        setSpeed();
        setWallForceMultiplier(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        

        if (IsPlayer(collision.gameObject))
        {
            Debug.Log("Collided with player");
            hunterAudioSource.PlayOneShot(hunterHitClip);
        }
        


    }


}
