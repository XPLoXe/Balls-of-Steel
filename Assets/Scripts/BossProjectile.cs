using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody projectileRB;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        projectileRB = GetComponent<Rigidbody>();

        projectileRB.AddForce(lookDirection * 10, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
