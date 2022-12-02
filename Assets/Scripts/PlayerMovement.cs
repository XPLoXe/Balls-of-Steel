using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    //[SerializeField]private float addedSpeed = 4;

    void Awake()
    {
        speed = PlayerDataManager.Instance.Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        //addedSpeed = 3.5f;
    }



    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical"); //for up and down
        float horizontalInput = Input.GetAxis("Horizontal"); //for left and right



        playerRb.AddForce(Vector3.forward * verticalInput * speed * Time.deltaTime, ForceMode.Impulse);
        playerRb.AddForce(Vector3.right * horizontalInput * speed * Time.deltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //float verticalInput = Input.GetAxis("Vertical"); //for up and down
        //float horizontalInput = Input.GetAxis("Horizontal"); //for left and right

        //playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime * 250);
        //playerRb.AddForce(focalPoint.transform.right * rightInput * speed * Time.deltaTime * 250);

        //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        //playerRb.AddForce(Vector3.forward * verticalInput * speed * Time.deltaTime * 250);
        //playerRb.AddForce(Vector3.right * horizontalInput * speed * Time.deltaTime * 250);

        //playerRb.AddForce(Vector3.forward * verticalInput * speed * Time.deltaTime * 250);
        //playerRb.AddForce(Vector3.right * horizontalInput * speed * Time.deltaTime * 250);

        


        //if (Input.GetKey(KeyCode.W))
        //{
        //    Debug.Log("W pressed");
        //    playerRb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log("S pressed");
        //    playerRb.AddForce(-Vector3.forward * speed * Time.deltaTime, ForceMode.Acceleration);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("D pressed");
        //    playerRb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Acceleration);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("A pressed");
        //    playerRb.AddForce(-Vector3.right * speed * Time.deltaTime, ForceMode.Acceleration);
        //}



    }
}
