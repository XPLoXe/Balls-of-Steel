using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveMaterial : MonoBehaviour
{
    private Color emColor;
    private Renderer rend; 

    // Start is called before the first frame update
    void Start()
    {
        emColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        DynamicGI.SetEmissive(rend, emColor);
    }

}
