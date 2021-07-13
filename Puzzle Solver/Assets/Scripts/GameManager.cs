using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Pencile;
    public GameObject TrailPath;
    // Start is called before the first frame update
    void Start()
    {
        Pencile.GetComponent<Animator>().enabled = false;
        TrailPath.GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pencile.GetComponent<Animator>().enabled = true;
            TrailPath.SetActive(false);
        }
    }
}
