using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Enemy
{
    //AUDIO\\
    private AudioSource hunterAudioSource;
    public AudioClip hunterHitClip;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.4f);
        hunterAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        switch (MainManager.Instance.difficulty)
        {
            case 1:
                setSpeed(10.0f);    //Easy
                setWallForceMultiplier(0.4f);
                break;
            case 2:
                setSpeed(3.0f);    //God
                setWallForceMultiplier(1.0f);
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
