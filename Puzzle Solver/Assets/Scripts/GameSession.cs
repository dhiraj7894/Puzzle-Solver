using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Cinemachine;


public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI status;
    [SerializeField] int totalPencilsToBeCollected = 5;
    [SerializeField] int numPencilsCollected = 0;
   // [SerializeField] CinemachineVirtualCamera finishCam;
    Rotator rotator;
    bool arePencilsCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        rotator = FindObjectOfType<Rotator>();
    }

    // Update is called once per frame
    void Update()
    {
        status.text = numPencilsCollected.ToString() + " / " + totalPencilsToBeCollected.ToString();

       
    }

    public void AddPencil()
    {
        numPencilsCollected++;
        rotator.Switch();
        if (numPencilsCollected >= totalPencilsToBeCollected)
        {
            arePencilsCollected = true;
            rotator.Finish();
        }
    }

    public bool AreAllPencilsCollected()
    {
        return arePencilsCollected;
    }
}
