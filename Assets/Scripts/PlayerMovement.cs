using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); //for up and down
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        float rightInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * rightInput * speed);
    }
}
