using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    Rotator rotator;
    // Start is called before the first frame update
    void Start()
    {
        rotator = FindObjectOfType<Rotator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pencil")
        {
            rotator.Switch();
        }
    }
}
