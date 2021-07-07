using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Rotator : MonoBehaviour
{
    [SerializeField] Transform pivot1;
    [SerializeField] Transform pivot2;
    [SerializeField] float rotateAmount = 90f;
    [SerializeField] int pivotSwitch = -1;
    [SerializeField] GameObject[] blockers;
    [SerializeField] List<GameObject> pencils;
    [SerializeField] CinemachineVirtualCamera finishCam;
    [SerializeField] CinemachineVirtualCamera rotarCam;
    [SerializeField] float rotateSpeedFactor = 0.5f;
    [SerializeField] Animator levelComplete;
    [SerializeField] GameObject collectAllPencilText;
    [SerializeField] GameObject confetti;
    [SerializeField] List<GameObject> pencilsActive;
    [SerializeField] GameObject stensil;
    int idx;
    int rotarCamSwitch = -1;
    int rotationClock = 1;
    bool isMoving = true;
    float timer = 1;
    GameSession gameSession;
    bool activateTimer = false;
    // int rotarCamIdx = 0;
   // int x = 2;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        SpawnFirstPencil();
       // x += ++x - x++ + x - --x;
    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (activateTimer)
        {
            timer = timer - Time.deltaTime;
            if(timer <= 0)
            {
                timer = 1;
                activateTimer = false;
            }
        }


        if(isMoving)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                pivotSwitch *= -1;
                rotationClock *= -1;
            }

            if (pivotSwitch > 0)
            {

                this.transform.RotateAround(pivot1.position,
                new Vector3(0f, 1f, 0f) * rotationClock,
                rotateAmount * Time.deltaTime);
            }

            else if (pivotSwitch < 0)
            {
                this.transform.RotateAround(pivot2.position,
                new Vector3(0f, 1f, 0f) * rotationClock,
                rotateAmount * Time.deltaTime);
            }
        }
        
      
    }

    public void Switch()
    {
        pivotSwitch *= -1;
        rotationClock *= -1;
    }



    public void Finish()
    {
        /*
         foreach(GameObject blocker in blockers)
         {
             blocker.SetActive(false);
         }
         */
        isMoving = false;
       finishCam.Priority = 100;
        stensil.SetActive(false);
        DeactivatePencils();
       StartCoroutine(LevelComplete());
    }

    private void DeactivatePencils()
    {
       foreach(GameObject pencil in pencilsActive)
        {
            pencil.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(1f);
        
        collectAllPencilText.SetActive(false);
        levelComplete.gameObject.SetActive(true);
        levelComplete.SetTrigger("PopUp");
        confetti.SetActive(true);
    }

    public void SpeedUp()
    {
        rotateAmount += rotateSpeedFactor;
    }

    public void ZoomOut()
    {
        if (!activateTimer)
        {
            activateTimer = true;
            rotarCamSwitch *= -1;
           // Debug.Log("Before : " + rotarCam.Priority);
            // rotarCamIdx++;
            if (rotarCamSwitch > 0)
            {
                rotarCam.Priority = 6;
            }

            else
            {
                rotarCam.Priority = 12;
            }

           // Debug.Log("After : " + rotarCam.Priority);
        }
       
        // rotarCam= 12 + rotarCamIdx;
        //  rotarCam[rotarCamIdx - 1].Priority = 9;
        
    }

    public void ActivateNewPencil(GameObject gameObject)
    {
        //Debug.Log("aCTIVATE");
        pencils.Remove(gameObject);

        if(!gameSession.AreAllPencilsCollected())
        {
           // int idx = Random.Range(0, pencils.Count - 1);
            // Debug.Log(idx);
            GameObject pencil = pencils[idx];
            pencil.SetActive(true);
            StartCoroutine(pencil.GetComponent<Pencil>().SpawnComplete());
            idx++;
            pencilsActive.Add(pencil);
        }
        
    }
    
   void SpawnFirstPencil()
    {
        // int idx = Random.Range(0, pencils.Count - 1);
        /*float posX, posZ;
        do
        {
            posX = Random.Range(pivot1.position.x, pivot2.position.x);
        }
        while (posX == pivot1.position.x || posX == pivot2.position.x);

        do
        {
            posZ = Random.Range(pivot1.position.z, pivot2.position.);
        }
        while (posX == pivot1.position.x || posX == pivot2.position.x);
        
        posZ = Random.Range(pivot1.position.z, pivot2.position.z);
        */

        
        GameObject pencil = pencils[idx];
        pencil.SetActive(true);
        StartCoroutine(pencil.GetComponent<Pencil>().SpawnComplete());
        idx++;
        pencilsActive.Add(pencil);
    }

}
