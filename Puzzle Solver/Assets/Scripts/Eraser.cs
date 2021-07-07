using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pencil")
        {
            //etComponent<Collider>().isTrigger = false;
            //GetComponent<Rigidbody>().isa
            other.GetComponent<Pencil>().Remove();
        }


    }
    
}
