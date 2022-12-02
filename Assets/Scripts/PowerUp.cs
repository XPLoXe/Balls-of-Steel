using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   

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


        
    }

    private void Awake()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
