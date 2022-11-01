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
    
    private AudioSource enemyAudioSource;
    public AudioClip enemyFallClip;
    private AudioClip enemyHitClip;
    [SerializeField] private bool isGrounded = true; 

    public static int difficulty;

    

    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        setSpeed();
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {

        

        //EasyMovement(); //only for testing. remove otherwise
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

   

    protected bool IsPlayer(GameObject collision)
    {
        return collision.gameObject.CompareTag("Player");
    }

    protected bool IsGrounded(GameObject ground)
    {
        if (ground.gameObject.CompareTag("Ground"))
        {
            return true;
        }

        return false;
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        setGrounded(IsGrounded(collision.gameObject));

        if (collision.gameObject.CompareTag("Player")){
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = (collision.gameObject.transform.position - transform.position);
            playerRB.AddForce(awayFromEnemy * 2f, ForceMode.Impulse);
            //playerRB.AddExplosionForce(30f, awayFromEnemy, 5f, 10f);
            //enemyRb.AddExplosionForce(30f, -LookDirection(), 5f, 10f);
            
        }

        

    }

    public virtual void OnTriggerEnter(Collider other)
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

    public void ExplosionPhysics(GameObject receiver)
    {
        Rigidbody playerRB = receiver.GetComponent<Rigidbody>();
        //playerRB.AddExplosionForce(400f, AwayFromEnemy(receiver), 50.0f);
        //playerRB.AddExplosionForce(400f, Vector3.up, 10.0f);
    }

    public Vector3 AwayFromEnemy(GameObject receiver)
    {
        return (receiver.transform.position - transform.position).normalized;
    }

    public virtual void setSpeed()
    {
        if (difficulty == 2)
        {
            speed = 2.5f;
        }
    }

    public void setSpeed(float value) //overload
    {
        speed = value;
    }

    public virtual void setWallForceMultiplier(float multiplier)
    {
        wallForceMuliplier = multiplier;
    }

    protected bool getGrounded()
    {
        return isGrounded;
    }

    protected void setGrounded(bool flag)
    {
        isGrounded = flag;
    }
}
