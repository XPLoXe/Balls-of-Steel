using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private GameObject player;
    private Rigidbody enemyRb;
    private float lowerBounds = 20.0f;
    private float waterBounds = 7.0f;
    private AudioSource enemyAudioSource;
    public AudioClip enemyFallClip;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        //Enemy following the player
        enemyRb.AddForce(lookDirection * speed);
    }

    void Update()
    {
        if (transform.position.y == -waterBounds)
        {
            enemyAudioSource.PlayOneShot(enemyFallClip);
        }

        if (transform.position.y < -lowerBounds)
        {

            Destroy(gameObject);
        }
    }
}
