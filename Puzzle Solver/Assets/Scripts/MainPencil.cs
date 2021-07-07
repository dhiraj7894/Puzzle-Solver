using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPencil : MonoBehaviour
{
    [SerializeField] int pencilArrayLength = 5;
    [SerializeField] GameObject[] pencils;
    [SerializeField] Transform [] pencilSlots;
    [SerializeField] GameObject rotator;

    bool zoomOut = false;
    //Rotator rotator;

    int switchSide = 1;
    int i = 1;
    GameSession gameSession;
   // int idx = 1;
   
    // Start is called before the first frame update
    void Start()
    {
        //rotator = FindObjectOfType<Rotator>();
        pencils = new GameObject[pencilArrayLength];
        pencils[4] = gameObject;

        gameSession = FindObjectOfType<GameSession>();
        //baseIndex = pencilArrayLength / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(zoomOut)
        {
            rotator.GetComponent<Rotator>().ZoomOut();
            rotator.GetComponent<Rotator>().SpeedUp();
            zoomOut = false;
        }
        
    }

    public int GetEmptyCell(GameObject obj)
    {
        zoomOut = true;
        gameSession.AddPencil();
        rotator.GetComponent<Rotator>().ActivateNewPencil(obj);
        
       
        for(int i = pencilArrayLength - 1; i >= 0; i--)
        {
            if(pencils[i] == null)
            {
                return i;
            }
        }
       

        return -1;
    }

    public void AddPencil(int emptyCell, GameObject pencil)
    {
        pencils[emptyCell] = pencil;
    }

    public Transform[] GetPencilSlots()
    {
        return pencilSlots;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("SDFsdfsdf");
        if(other.tag == "Pencil")
        {
            other.transform.GetComponent<Collider>().isTrigger = false;
            //other.transform.parent = transform.parent;
            int emptyCell;
            emptyCell = GetEmptyCell(gameObject);
            if (emptyCell >= 0)
            {
                //Debug.Log("Empty cell : " + emptyCell);
                AddPencil(emptyCell, other.gameObject);
                other.GetComponent<Pencil>().ActivatePencil(pencilSlots[emptyCell], gameObject);
            }

        }


    }

    public void DeletePencil(GameObject gameObject)
    {
       for(int i = 0; i < pencilArrayLength; i++)
        {
            if(pencils[i] == gameObject)
            {
                pencils[i] = null;
                Debug.Log("Deleted at idx : " + i);
            }
        }
    }

    public void Sort()
    {
        for(int i = pencilArrayLength-1; i >=1; i--)
        {
            bool elementFound = true;
            if(pencils[i] == null)
            {
                elementFound = false;
                for(int j = i - 1; j >= 0; j--)
                {
                    
                    if (pencils[j] != null)
                    {
                        
                        elementFound = true;
                        Debug.Log(elementFound +","+j);
                        //GameObject t = pencils[j];
                        //pencils[j] = null;
                        //pencils[i] = t;
                        pencils[i] = pencils[j];
                        pencils[j] = null;
                        pencils[i].GetComponent<Pencil>().ReOrder(true, pencilSlots[i]);
                        break;
                    }
                }

            }

            if (!elementFound)
                break;
        }
    }
    
}
