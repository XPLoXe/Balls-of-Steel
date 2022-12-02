using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{

    //boundaries\\
    private float lowerBounds = 20.0f;
    private float waterBounds = 7.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WaterBounds();
        LowerBounds();
    }

    private void WaterBounds()
    {
        if (transform.position.y == -waterBounds)
        {
            //enemyAudioSource.PlayOneShot(enemyFallClip);
        }
    }

    private void LowerBounds()
    {
        if (transform.position.y < -lowerBounds)
        {
            if (gameObject.CompareTag("Boss"))
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }

    }
}
