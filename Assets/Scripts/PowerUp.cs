using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //User Inputs
    public float rotationSpeed = 50.0f;
    //public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    //Position Storage Variables
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {

        if (gameObject.CompareTag("Gem"))
        {
            Destroy(gameObject, 5.0f);
        }
        else
        {
            Destroy(gameObject, 20.0f);
        }


        //Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    private void Awake()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);

        //float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

}
