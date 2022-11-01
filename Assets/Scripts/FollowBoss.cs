using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoss : MonoBehaviour
{
    public GameObject boss;

    //User Inputs
    public float rotationSpeed = 50.0f;
    //public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 tempPos = new Vector3(0, 4, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //item.transform.Translate(transform.position + new Vector3 (0, 4, 0));
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        //transform.position = boss.transform.position + new Vector3 (0, 4, 0);

        tempPos.x = boss.transform.position.x;
        tempPos.z = boss.transform.position.z;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
