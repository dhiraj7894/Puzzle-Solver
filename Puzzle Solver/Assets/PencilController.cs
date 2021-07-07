using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilController : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("Draw");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
