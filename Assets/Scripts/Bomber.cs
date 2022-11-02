using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bomber : Enemy
{
    public ParticleSystem explosionParticle;

    //Audio\\
    private AudioSource bomberAudioSource;
    public AudioClip bomberExplosionClip;
    public AudioClip bomberHitClip;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player");
        //explosionParticle = GetComponent<ParticleSystem>();
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        

        switch (MainManager.Instance.difficulty)
        {
            case 1:
                setSpeed(20.0f);    //Easy
                setWallForceMultiplier(1.5f);
                break;
            case 2:
                setSpeed(3.0f);    //God
                setWallForceMultiplier(1f);
                break;
            default:
                setSpeed(10f);      //testing
                setWallForceMultiplier(7.0f);
                break;
        }

    }

    private void Awake()
    {
        bomberAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (IsPlayer(collision.gameObject))
        {
            //AudioSource.PlayClipAtPoint(bomberExplosionClip, this.gameObject.transform.position);
            bomberAudioSource.PlayOneShot(bomberExplosionClip);


            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();

            //explosionParticle.Play();
            DetachParticles();
            explosionParticle.Play();


            StartCoroutine(WaitForDestroy());
            playerRB.AddForce(AwayFromEnemy(collision.gameObject) * 5f, ForceMode.Impulse);
            //Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
        }
        else
        {
            bomberAudioSource.PlayOneShot(bomberHitClip);
        }
    }

    IEnumerator WaitForDestroy()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    
    public void DetachParticles()
    {
        explosionParticle.transform.parent = null;
        //explosionParticle.Play();
    }


}
