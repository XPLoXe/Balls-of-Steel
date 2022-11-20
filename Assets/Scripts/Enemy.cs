using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float wallForceMuliplier;
    protected GameObject player;
    protected Rigidbody enemyRb;
    
    private AudioSource enemyAudioSource;
    public AudioClip enemyFallClip;
    private AudioClip enemyHitClip;
    [SerializeField] private bool isGrounded = true; 

    public static int difficulty;

    private Action Movement;
    private delegate void MovementDelegate();
    MovementDelegate movementType;


    // Start is called before the first frame update
    void Start()
    {
        //yield return new WaitForSeconds(10);
        //player = GameObject.Find("Player");



        //difficulty = MainManager.Instance.difficulty;
        //Debug.Log(difficulty);

        //Debug.Log(MainManager.Instance.difficulty);

        //switch (MainManager.Instance.difficulty)
        //{
        //    case 1:     //Easy
        //        Movement = EasyMovement;

        //        break;
        //    case 2:     //God
        //        Movement = HardMovement;
        //        Debug.Log("it got in");
        //        break;
        //    default:    //testing
        //        Movement = EasyMovement;
        //        break;
        //}

        //if (difficulty == 1)
        //{
        //    //Movement = EasyMovement;
        //    movementType = EasyMovement;
        //    Debug.Log("it got in 1");
        //}
        //else
        //{
        //    //Movement = HardMovement;
        //    Debug.Log("it got in 2");
        //    movementType = HardMovement;
        //}

        //Debug.Log(Movement);

    }

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyRb = GetComponent<Rigidbody>();


        //if (MainManager.Instance.difficulty == 1)
        //{
        //    //Movement = EasyMovement;
        //    movementType = EasyMovement;
        //    Debug.Log("it got in 1");
        //}
        //else
        //{
        //    //Movement = HardMovement;
        //    Debug.Log("it got in 2");
        //    movementType = HardMovement;
        //}
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(MainManager.Instance.difficulty);
        ////movementType = EasyMovement;
        //if (isGrounded == true)
        //{
        //    movementType();

        //    //if (MainManager.Instance.difficulty == 1)
        //    //{
        //    //    EasyMovement();
        //    //}
        //    //else
        //    //{
        //    //    HardMovement();
        //    //}


        //    //Movement();
        //}s

        Debug.Log(MainManager.Instance.difficulty);
        Debug.Log(isGrounded);


        if (isGrounded)
        {
            if (MainManager.Instance.difficulty == 1)
            {
                EasyMovement();
            }
            else
            {
                HardMovement();
            }
        }
    }


    void Update()
    {

        

        

    }

    private void LateUpdate()
    {
        //if (isGrounded == true)
        //{
        //    movementType();

        //    //if (MainManager.Instance.difficulty == 1)
        //    //{
        //    //    EasyMovement();
        //    //}
        //    //else
        //    //{
        //    //    HardMovement();
        //    //}


        //    //Movement();
        //}
    }


    private void ExecuteMovement (MovementDelegate movementType)
    {
        movementType();
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
