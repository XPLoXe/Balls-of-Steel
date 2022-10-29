using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 3.0f;
    [SerializeField] protected float wallForceMuliplier = 5.0f;
    protected GameObject player;
    protected Rigidbody enemyRb;
    
    protected AudioSource enemyAudioSource;
    public AudioClip enemyFallClip;
    public AudioClip enemyHitClip;
    [SerializeField] private bool isGrounded = true; 

    public static int difficulty;

    //boundaries\\
    private float lowerBounds = 20.0f;
    private float waterBounds = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        setSpeed();
    }

    public virtual void setSpeed()
    {
        if (difficulty == 2)
        {
            speed = 2.5f;
        }
    }

    public virtual void setWallForceMultiplier(float multiplier)
    {
        wallForceMuliplier = multiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded == true)
        {
            if (difficulty == 1)
            {
                EasyMovement();
            }

            if (difficulty == 2)
            {
                HardMovement();
            }
        }
        
        
    }


    void Update()
    {

        WaterBounds();
        LowerBounds();
        
    }

    protected void WaterBounds()
    {
        if (transform.position.y == -waterBounds)
        {
            enemyAudioSource.PlayOneShot(enemyFallClip);
        }
    }

    protected void LowerBounds()
    {
        if (transform.position.y < -lowerBounds)
        {

            Destroy(gameObject);
        }
    }

    private void HardMovement()
    {
        Vector3 lookDirection = (player.transform.position - transform.position);
        //Enemy following the player
        enemyRb.AddForce(lookDirection * speed);

    }

    private void EasyMovement()
    {
        //Enemy following the player
        enemyRb.AddForce(LookDirection() * speed);
    }

    protected void AvoidEdgeImpulse(Vector3 direction)
    {
        
        //Enemy following the player
        //enemyRb.AddForce(lookDirection * speed, ForceMode.Impulse);
        enemyRb.AddForce(direction * wallForceMuliplier, ForceMode.Impulse);
    }

    protected Vector3 LookDirection()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        return lookDirection;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            enemyAudioSource.PlayOneShot(enemyHitClip);
        }
    }

    protected void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    protected void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = (collision.gameObject.transform.position - transform.position);
            playerRB.AddExplosionForce(30f, awayFromEnemy, 5f, 10f);
            enemyRb.AddExplosionForce(30f, -LookDirection(), 5f, 10f);
            
        }



    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            AvoidEdgeImpulse(other.transform.up);
        }

    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            AvoidEdgeImpulse(other.transform.up);
        }
    }

}
