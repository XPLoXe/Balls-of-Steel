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

        switch (MainManager.Instance.difficulty)
        {
            case 1:
                setSpeed(10.0f);    //Easy
                setWallForceMultiplier(7.0f);
                break;
            case 2:
                setSpeed(15.0f);    //God
                setWallForceMultiplier(14.0f);
                break;
            default:
                setSpeed(10f);      //testing
                setWallForceMultiplier(7.0f);
                break;
        }
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
